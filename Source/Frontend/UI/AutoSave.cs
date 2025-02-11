// <copyright file="AutoSave.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI.Components.Controls;
using Timer = System.Timers.Timer;

namespace RTCV.UI
{
    public static class AutoSave
    {
        private static readonly Timer AutoSaveTimer;
        private static bool InProgress;

        private static readonly object InProgressLock = new object();
        public static readonly object SavingLock = new object();
        

        private static bool _saveStatesUnsaved;
        public static bool SaveStatesUnsaved
        {
            get
            {
                lock (SavingLock)
                {
                    return _saveStatesUnsaved;
                }
            }
            set
            {
                lock (SavingLock)
                {
                    _saveStatesUnsaved = value;
                }
            }
        }

        private static bool _stockpileUnsaved;
        public static bool StockpileUnsaved
        {
            get
            {
                lock (SavingLock)
                {
                    return _stockpileUnsaved;
                }
            }
            set
            {
                lock (SavingLock)
                {
                    _stockpileUnsaved = value;
                }
            }
        }

        static AutoSave()
        {
            AutoSaveTimer = new Timer { AutoReset = true };
            AutoSaveTimer.Elapsed += PerformAutoSave;
        }

        public static void SetInterval(int seconds)
        {
            AutoSaveTimer.Interval = seconds * 1000;
        }
        public static void Start()
        {
            AutoSaveTimer.Start();
        }
        public static void Stop()
        {
            AutoSaveTimer.Stop();
        }

        private static async void PerformAutoSave(object sender, ElapsedEventArgs e)
        {
            Stop();
            lock (InProgressLock)
            {
                if (InProgress || S.ISNULL<SavestateManagerForm>() || S.ISNULL<StockpileManagerForm>())
                {
                    Start();
                    return;
                }
                InProgress = true;
            }
    
            if (SaveStatesUnsaved)
            {
                var statesForm = S.GET<SavestateManagerForm>();
                string[] allSSKs = Directory.GetFiles(RtcCore.AutoSaveDir, "*.ssk");
                while (allSSKs.Length >= 3)
                {
                    string oldestSSK = allSSKs.OrderBy(f => new FileInfo(f).CreationTime).First();
                    File.Delete(oldestSSK);
                    allSSKs = Directory.GetFiles(RtcCore.AutoSaveDir, "*.ssk");
                }
                    
                string savePath = Path.Combine(RtcCore.AutoSaveDir, $"autosave_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.ssk");

                Console.WriteLine("Auto-saving save states...");
                Stopwatch sw = Stopwatch.StartNew();
                var tcs = new TaskCompletionSource<bool>();

                Form sync = SyncObjectSingleton.SyncObject;
                
                SyncObjectSingleton.FormBeginExecute(() =>
                {
                    Toast toast = new Toast("Auto-saving save states...", "");
                    statesForm.ParentCanvas.ShowToast(toast);

                    Task.Run(() =>
                    {
                        lock (SavingLock)
                        {
                            statesForm.SaveSSK(savePath);
                        }
                        tcs.SetResult(true);
                    }).ContinueWith(_ =>
                    {
                        // Close the toast on the UI thread
                        toast.Close();
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                });

                await tcs.Task;
                sw.Stop();
                Console.WriteLine("Auto-save complete. Took " + sw.ElapsedMilliseconds + "ms");
                SaveStatesUnsaved = false;
            }
                
            // if stockpile is unsaved and not using a core that must be restarted to save the stockpile
            if (StockpileUnsaved && !(AllSpec.VanguardSpec[VSPEC.CORE_DISKBASED] as bool? ?? false))
            {
                var stockpileForm = S.GET<StockpileManagerForm>();
                string[] allStockpiles = Directory.GetFiles(RtcCore.AutoSaveDir, "*.sks");
                while (allStockpiles.Length >= 3)
                {
                    string oldestSSK = allStockpiles.OrderBy(f => new FileInfo(f).CreationTime).First();
                    File.Delete(oldestSSK);
                    allStockpiles = Directory.GetFiles(RtcCore.AutoSaveDir, "*.sks");
                }
                    
                string savePath = Path.Combine(RtcCore.AutoSaveDir, $"autosave_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.sks");

                var tcs = new TaskCompletionSource<bool>();
                
                SyncObjectSingleton.FormBeginExecute(() =>
                {
                    Stockpile sks = new Stockpile(stockpileForm.dgvStockpile);

                    if (sks.StashKeys.Count > 0)
                    {
                        Toast toast = new Toast("Auto-saving stockpile...", "");
                        stockpileForm.ParentCanvas.ShowToast(toast);
                        Task.Run(() =>
                        {
                            lock (SavingLock)
                            {
                                Stockpile.Save(sks, savePath, false, Params.IsParamSet("COMPRESS_STOCKPILE"));
                            }
                            tcs.SetResult(true);
                        }).ContinueWith(_ =>
                        {
                            // Close the toast on the UI thread
                            toast.Close();
                        }, TaskScheduler.FromCurrentSynchronizationContext());
                    }
                });
                StockpileUnsaved = false;
            }

            lock (InProgressLock)
            {
                InProgress = false;
            }
            Start();
        }
    }
}
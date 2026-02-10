namespace RTCV.Plugins.HexEditor
{
    using System;
    using RTCV.Common;
    using RTCV.CorruptCore;
    using RTCV.NetCore;

    public class HexEditorConnector : IRoutable
    {
        public HexEditorConnector()
        {
            LocalNetCoreRouter.registerEndpoint(this, "HEXEDITOR");
        }
        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            //Use setReturnValue to handle returns
            var message = e.message;
            var advancedMessage = message as NetCoreAdvancedMessage;

            switch (e.message.Type)
            {
                case NetCore.Commands.Remote.OpenHexEditor:
                    {
                        SyncObjectSingleton.FormExecute(() =>
                        {
                            if (S.ISNULL<HexEditor>())
                            {
                                S.SET(new HexEditor());
                            }
                            S.GET<HexEditor>().Restart();
                            // Why does this form refuse to be on top unless it's restored? ¯\_(ツ)_/¯
                            S.GET<HexEditor>().WindowState = System.Windows.Forms.FormWindowState.Minimized;
                            S.GET<HexEditor>().Show();
                            S.GET<HexEditor>().WindowState = System.Windows.Forms.FormWindowState.Normal;
                        });
                    }
                    break;

                case NetCore.Commands.Emulator.OpenHexEditorAddress:
                    {
                        var temp = advancedMessage.objectValue as object[];
                        var domain = (string)temp[0];
                        var address = (long)temp[1];

                        MemoryInterface mi = MemoryDomains.GetInterface(domain);
                        if (mi == null)
                        {
                            break;
                        }

                        SyncObjectSingleton.FormExecute(() =>
                        {
                            if (!S.GET<HexEditor>().Visible)
                                S.GET<HexEditor>().WindowState = System.Windows.Forms.FormWindowState.Minimized;

                            if (S.GET<HexEditor>().IsDisposed)
                            {
                                S.SET(new HexEditor());
                            }
                            S.GET<HexEditor>().Restart();
                            S.GET<HexEditor>().Show();
                            S.GET<HexEditor>().WindowState = System.Windows.Forms.FormWindowState.Normal;
                            S.GET<HexEditor>().SetDomain(mi);
                            S.GET<HexEditor>().GoToAddress(address);
                        });
                    }
                    break;
            }
            return e.returnMessage;
        }
    }
}

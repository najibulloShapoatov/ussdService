using System;
using System.IO;
using log4net.Config;
using Topshelf;

namespace BabilonUSSD
{
    static class Program
    {
        static void Main()
        { 
            
            XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "log4net.config")));

            Host h = HostFactory.New(x =>
            {
                x.Service<UssdService>(s =>
                {
                    s.ConstructUsing(name => new UssdService());
                    s.WhenStarted(wd => wd.Start());
                    s.WhenStopped(wd => wd.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("Babilon-M.KasperskyKEY");
                x.SetDisplayName("Babilon-M.KasperskyKEY");
                x.SetServiceName("Babilon-M.KasperskyKEY");
            });

            h.Run();
        }
    }
}

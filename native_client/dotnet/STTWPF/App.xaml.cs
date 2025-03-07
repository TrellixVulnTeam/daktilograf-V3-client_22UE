﻿using CommonServiceLocator;
using STT.WPF.ViewModels;
using STTClient.Interfaces;
using GalaSoft.MvvmLight.Ioc;
using System.Windows;

namespace STTWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            try
            {
                //Register instance of STT
                STTClient.STT client =
                    new STTClient.STT("daktilograf-sr-F10.pbmm");

                SimpleIoc.Default.Register<ISTT>(() => client);
                SimpleIoc.Default.Register<MainWindowViewModel>();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
                Current.Shutdown();
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            //Dispose instance of STT
            ServiceLocator.Current.GetInstance<ISTT>()?.Dispose();
        }
    }
}

﻿using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using FlashbackUwp.Views;

namespace FlashbackUwp.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        public SettingsPartViewModel SettingsPartViewModel { get; } = new SettingsPartViewModel();
        public AboutPartViewModel AboutPartViewModel { get; } = new AboutPartViewModel();

        public async Task NavigateToManageForums()
        {
            await NavigationService.NavigateAsync(typeof(ManageForumlist));
        }
    }

    public class SettingsPartViewModel : ViewModelBase
    {
        Services.SettingsServices.SettingsService _settings;
        public ObservableCollection<string> FontSizes { get; set; }

        public SettingsPartViewModel()
        {            
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                // designtime
            }
            else
            {
                _settings = Services.SettingsServices.SettingsService.Instance;
            }

            FontSizes = new ObservableCollection<string> {"120%", "110%", "100%", "90%", "80%", "70%"};

        }        

        public string FontSize
        {
            get { return _settings.FontSize; }
            set
            {
                _settings.FontSize = value;
                
                RaisePropertyChanged();
            }
        }

        public bool HoppaTillSistaSidan
        {
            get { return _settings.HoppaTillSistaSidan; }
            set { _settings.HoppaTillSistaSidan = value; RaisePropertyChanged(); }
        }

        public bool ShowAvatars
        {
            get { return _settings.ShowAvatars; }
            set { _settings.ShowAvatars = value; RaisePropertyChanged(); }
        }

        public bool ShowSignatures
        {
            get { return _settings.ShowSignatures; }
            set { _settings.ShowSignatures = value; RaisePropertyChanged(); }
        }

        public bool UseLightThemeButton
        {
            get { return _settings.AppTheme.Equals(ApplicationTheme.Light); }
            set { _settings.AppTheme = value ? ApplicationTheme.Light : ApplicationTheme.Dark; RaisePropertyChanged(); }
        }

        public bool UseEmoticons
        {
            get { return _settings.UseEmoticons; }
            set { _settings.UseEmoticons = value; RaisePropertyChanged(); }
        }

        public bool UseSmartNavigation
        {
            get { return _settings.UseSmartNavigation; }
            set { _settings.UseSmartNavigation = value; RaisePropertyChanged(); }
        }
    }

    public class AboutPartViewModel : ViewModelBase
    {
        public Uri Logo => Windows.ApplicationModel.Package.Current.Logo;

        public string DisplayName => Windows.ApplicationModel.Package.Current.DisplayName;

        public string Publisher => Windows.ApplicationModel.Package.Current.PublisherDisplayName;

        public string Version
        {
            get
            {
                var v = Windows.ApplicationModel.Package.Current.Id.Version;
                return $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
        }

        public Uri RateMe => new Uri("http://aka.ms/template10");
    }
}

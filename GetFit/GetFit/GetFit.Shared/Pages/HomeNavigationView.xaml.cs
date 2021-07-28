﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using GetFit.Shared.Class;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GetFit.Shared.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomeNavigationView : Page
    {
        Session session;
        Member member;

        public HomeNavigationView()
        {
            this.InitializeComponent();
            session = new Session();
            member = session.getMemberLogin();
        }

        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            
            foreach (NavigationViewItemBase item in NavView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "home")
                {
                    NavView.SelectedItem = item;
                    NavView_Navigate(item as NavigationViewItem);
                    break;
                }
            }
        }

        private void NavView_Navigate(NavigationViewItem item)
        {
            if (member.type == "trainer")
            {
                switch (item.Tag)
                {
                    //case "Profile":
                    //    ContentFrame.Navigate(typeof(HomeMember));
                    //    break;
                    case "Exercises":
                        ContentFrame.Navigate(typeof(ExercisePage));
                        break;
                    case "Workouts":
                        ContentFrame.Navigate(typeof(WorkoutsPageTrainer));
                        break;
                    case "Settings":
                        ContentFrame.Navigate(typeof(Settings));
                        break;
                }
            }
            else if (member.type == "member")
            {
                if (member.premium == "no")
                {
                    switch (item.Tag)
                    {
                        case "Profile":
                            ContentFrame.Navigate(typeof(HomeMember));
                            break;
                        case "Exercises":
                            ContentFrame.Navigate(typeof(ExercisePage));
                            break;
                        case "Workouts":
                            ContentFrame.Navigate(typeof(WorkoutsPage));
                            break;
                        case "Settings":
                            ContentFrame.Navigate(typeof(Settings));
                            break;
                    }
                }
                if (member.premium == "yes")
                {

                }
            }

            
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
            NavView_Navigate(item as NavigationViewItem);
        }
    }
}

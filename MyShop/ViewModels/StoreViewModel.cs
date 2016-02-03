using Xamarin.Forms;
using Lotz.Xam.Messaging;
using MyShop.Helpers;
using Plugin.ExternalMaps;

namespace MyShop
{
    public class StoreViewModel : BaseViewModel
    {
        public Store Store { get; set; }
        public string Monday => string.Format("{0} - {1}", Store.MondayOpen, Store.MondayClose);
        public string Tuesday => string.Format("{0} - {1}", Store.TuesdayOpen, Store.TuesdayClose);
        public string Wednesday => string.Format("{0} - {1}", Store.WednesdayOpen, Store.WednesdayClose);
        public string Thursday => string.Format("{0} - {1}", Store.ThursdayOpen, Store.ThursdayClose);
        public string Friday => string.Format("{0} - {1}", Store.FridayOpen, Store.FridayClose);
        public string Saturday => string.Format("{0} - {1}", Store.SaturdayOpen, Store.SaturdayClose);
        public string Sunday => string.Format("{0} - {1}", Store.SundayOpen, Store.SundayClose);


        public string Address1 => Store.StreetAddress;
        public string Address2 => string.Format("{0}, {1} {2}", Store.City, Store.State, Store.ZipCode);

        public StoreViewModel(Store store, Page page) : base(page)
        {
            this.Store = store;
        }

        Command navigateCommand;
        public Command NavigateCommand
        {
            get
            {
                return navigateCommand ?? (navigateCommand = new Command(async () => await CrossExternalMaps.Current.NavigateTo(Store.Name, Store.Latitude, Store.Longitude)));
            }
        }

        Command callCommand;
        public Command CallCommand
        {
            get
            {
                return callCommand ?? (callCommand = new Command(() =>
                {
                    var phoneCallTask = MessagingPlugin.PhoneDialer;
                    if (phoneCallTask.CanMakePhoneCall)
                        phoneCallTask.MakePhoneCall(Store.PhoneNumber.CleanPhone());
                }));
            }
        }

    }
}


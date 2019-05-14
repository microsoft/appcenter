using Acquaint.ModelContracts;
using Newtonsoft.Json;

namespace Acquaint.Models
{
	public class Acquaintance : ObservableEntityData, IAcquaintance
    {
		public string DataPartitionId { get; set; }

        string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                SetProperty(ref _FirstName, value);
				// DisplayName is dependent on FirstName
                OnPropertyChanged(nameof(DisplayName));
				// DisplayLastNameFirst is dependent on FirstName
                OnPropertyChanged(nameof(DisplayLastNameFirst));
            }
        }

        string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set
            {
                SetProperty(ref _LastName, value);
				// DisplayName is dependent on LastName
                OnPropertyChanged(nameof(DisplayName));
				// DisplayLastNameFirst is dependent on LastName
                OnPropertyChanged(nameof(DisplayLastNameFirst));
            }
        }

        string _Company;
        public string Company
        {
            get { return _Company; }
            set { SetProperty(ref _Company, value); }
        }

        string _JobTitle;
        public string JobTitle
        {
            get { return _JobTitle; }
            set { SetProperty(ref _JobTitle, value); }
        }

        string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }

        string _Phone;
        public string Phone
        {
            get { return _Phone; }
            set { SetProperty(ref _Phone, value); }
        }

        string _Street;
        public string Street
        {
            get { return _Street; }
            set
            {
                SetProperty(ref _Street, value);
				// AddressString is dependent on Street
                OnPropertyChanged(nameof(AddressString));
            }
        }

        string _City;
        public string City
        {
            get { return _City; }
            set
            {
                SetProperty(ref _City, value);
				// AddressString is dependent on City
                OnPropertyChanged(nameof(AddressString));
            }
        }

        string _PostalCode;
        public string PostalCode
        {
            get { return _PostalCode; }
            set
            {
                SetProperty(ref _PostalCode, value);
				// AddressString is dependent on PostalCode
                OnPropertyChanged(nameof(AddressString));
				// StatePostal is dependent on PostalCode
                OnPropertyChanged(nameof(StatePostal));
            }
        }


        string _State;
        public string State
        {
            get { return _State; }
            set
            {
                SetProperty(ref _State, value);
				// AddressString is dependent on State
                OnPropertyChanged(nameof(AddressString));
				// StatePostal is dependent on State
                OnPropertyChanged(nameof(StatePostal));
            }
        }

        string _PhotoUrl;
        public string PhotoUrl
        {
            get { return _PhotoUrl; }
            set
            {
                SetProperty(ref _PhotoUrl, value);
				// SmallPhotoUrl is dependent on PhotoUrl
                OnPropertyChanged(nameof(SmallPhotoUrl));
            }
        }

        public string SmallPhotoUrl => PhotoUrl;

        [JsonIgnore]
        public string AddressString => string.Format(
            "{0} {1} {2} {3}",
            Street,
            !string.IsNullOrWhiteSpace(City) ? City + "," : "",
            State,
            PostalCode);

        [JsonIgnore]
        public string DisplayName => ToString();

        [JsonIgnore]
        public string DisplayLastNameFirst => $"{LastName}, {FirstName}";

        [JsonIgnore]
        public string StatePostal => State + " " + PostalCode;

		public override string ToString() => $"{FirstName} {LastName}";
    }
}


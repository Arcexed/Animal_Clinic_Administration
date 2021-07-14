using Animal_Clinic_Administration.Entity;
using Animal_Clinic_Administration.Models;
using Animal_Clinic_Administration.X;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;


namespace Animal_Clinic_Administration.ViewModels
{
    class AddViewModel : Base, IDataErrorInfo
    {
        public AddViewModel(MainViewModel obj)
        {
            mainViewModel = obj;
            InitCommands();
        }

        MainViewModel mainViewModel;
        public MainViewModel MainViewModel
        {
            get => mainViewModel;
            set => SetProperty(ref mainViewModel, value);
        }
        

        private void InitCommands()
        {
            CloseRegisterMenuCommand = new RelayCommand(ExecuteCloseRegisterMenu);
            ConfirmAddCommand = new RelayCommand(ExecuteConfirmAdding);
            AddPetsCommand = new RelayCommand(ExecuteAddPet);

        }
        public bool RegVis
        {
            get => MainViewModel.S_RegisterVis;
            set { MainViewModel.S_RegisterVis = value; OnPropertyChanged(); }

        }
        public bool DatVis
        {
            get => MainViewModel.DataGrid1Vis;
            set { MainViewModel.DataGrid1Vis = value; OnPropertyChanged(); }
        }
        public ICommand AddPetsCommand { get; private set; }

        public ICommand ConfirmAddCommand { get; private set; }

        public ICommand CloseRegisterMenuCommand { get; private set; }

        private void ExecuteCloseRegisterMenu()
        {
            RegVis = false;
            DatVis = true;
            PushDataGrid = false;
            ClearAddStrings();
        }
        public string Error
        {
            get { return null; }
        }
        public string this[string columnName]
        {
            get
            {
                string res = null;

                switch (columnName)
                {
                    case "nFirstName":
                        if (string.IsNullOrWhiteSpace(nFirstName)) res = "First name cannot be empty";
                        break;
                    case "nLastName":
                        if (string.IsNullOrWhiteSpace(nLastName)) res = "Last name cannot be empty";
                        break;
                    case "nPhone":
                        if (string.IsNullOrWhiteSpace(nPhone)) res = "Phone cannot be empty";
                        break;

                    case "pName":
                        if (string.IsNullOrWhiteSpace(pName)) res = "Pet's Name cannot be empty";
                        break;

                    case "pType":
                        if (string.IsNullOrWhiteSpace(pType)) res = "Pet's Type cannot be empty";
                        break;

                }
                if (ErrorCollection.ContainsKey(columnName))
                    ErrorCollection[columnName] = res;
                else if (res != null)
                    ErrorCollection.Add(columnName, res);
                return res;
            }
        }

        MyPerson selectedPersonForUpdate;
        public MyPerson SelectedPersonForUpdate
        {
            get => selectedPersonForUpdate;
            set => SetProperty(ref selectedPersonForUpdate, value);
        }
        bool CheckNewData()
        {
            if (string.IsNullOrEmpty(nFirstName) || string.IsNullOrEmpty(nLastName) || string.IsNullOrEmpty(nPhone)
               || string.IsNullOrEmpty(pName) || string.IsNullOrEmpty(pType))
            {
                MessageBox.Show("Please fill in the required fields");
                return false;
            }
            else if (!StaticFunctions.ValidTelephone(nPhone))
            {
                MessageBox.Show("Please enter valid phone ");
                return false;
            }
            else if (!string.IsNullOrEmpty(pBirth))
            {
                if (!StaticFunctions.DateChecker(pBirth))
                {
                    MessageBox.Show("Please enter valid date");
                    return false;
                }
            }
            else
            {
                pBirth = null;
                return true;
            }
            return true;
        }
        void ClearAddStrings()
        {
            nFirstName = null;
            nLastName = null;
            nAge = null;
            nPhone = null;
            nMail = null;
            pName = null;
            pType = null;
            pBirth = null;
            pPassport = null;
        }

        void ExecuteConfirmAdding()
        {
            if (CheckNewData())
            { 
                using (my_clinicEntities db = new my_clinicEntities())
                {
                    var LastPerId = db.Person.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                    var LastPetId = db.Pet.OrderByDescending(p => p.Per_Id).FirstOrDefault().P_Id;
                    var FindEx = db.Person.Where(p => p.phone == nPhone).ToList();
                    if (SelectedPersonForUpdate != null)
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.Pet.Add(new Pet()
                                {

                                    Per_Id = SelectedPersonForUpdate.Id,
                                    P_Id = LastPetId + 1,
                                    name = pName,
                                    birth = Convert.ToDateTime(pBirth),
                                    type = pType,
                                    passport_number = pPassport

                                });
                                db.SaveChanges();
                                transaction.Commit();
                                PushDataGrid = false;
                                MessageBox.Show("Successfully Completed");
                                MainViewModel.Db_GetData();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                            }
                        }
                    }
                    else MessageBox.Show("Choose Person");
                }
            }
        }

        void ExecuteAddPet()
        {
            if (CheckNewData())
            {
                Finded = null;
                PushDataGrid = false;
                using (my_clinicEntities db = new my_clinicEntities())
                {
                    var LastPerId = db.Person.OrderByDescending(p => p.Id).FirstOrDefault().Id;
                    // var LastPetId = db.Pet.OrderByDescending(p => p.P_Id).FirstOrDefault().P_Id;
                    var FindEx = db.Person.Where(p => p.phone == nPhone).FirstOrDefault();
                    if (FindEx == null)
                    {
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                db.Person.Add(new Person
                                {

                                    first_name = nFirstName,
                                    last_name = nLastName,
                                    age = nAge,
                                    mail = nMail,
                                    phone = nPhone,
                                    Pet = new List<Pet>()
                                    {
                                        new Pet()
                                        {

                                        Per_Id=LastPerId+1,

                                        name=pName,
                                        birth=Convert.ToDateTime(pBirth),
                                        type=pType,
                                        passport_number=pPassport
                                        }

                                    }

                                });
                                db.SaveChanges();
                                transaction.Commit();
                                MessageBox.Show("Successfully Completed");
                                MainViewModel.Db_GetData();
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();

                            }
                        }

                    }

                    else if (FindEx != null)
                    {
                        Finded = new ObservableCollection<MyPerson>();
                        Finded.Add(StaticFunctions.PersonToMyPerson(FindEx));
                        if (SelectedPersonForUpdate == null)
                        {
                            MessageBox.Show("Please select where to add ");
                        }
                        PushDataGridVisibility();
                    }
                }
            }
        }
        void CheckSelectedPersonForUpdate()
        {
            if (SelectedPersonForUpdate == null)
            {
                MessageBox.Show("Please Select A Person");
            }
        }

        ObservableCollection<MyPerson> finded;
        public ObservableCollection<MyPerson> Finded { get { return finded; } set { finded = value; OnPropertyChanged(); } }
        Dictionary<string, string> errorCollection = new Dictionary<string, string>();
        public Dictionary<string, string> ErrorCollection
        {
            get { return errorCollection; }
            set { errorCollection = value; OnPropertyChanged(); }
        }

        // Registration

        string nfirstName;
        public string nFirstName
        {
            get => nfirstName;
            set => SetProperty(ref nfirstName, value);
        }
        string nlastName;
        public string nLastName
        {
            get { return nlastName; }
            set => SetProperty(ref nlastName, value);
        }
        
        int? nage;
        public int? nAge
        {
            get => nage;
            set => SetProperty(ref nage, value);
        }

        string nphone;
        public string nPhone
        {
            get => nphone;
            set => SetProperty(ref nphone, value);
        }

        string nmail;
        public string nMail
        {
            get => nmail;
            set => SetProperty(ref nmail, value);
        }

        string pname;
        public string pName
        {
            get => pname;
            set => SetProperty(ref pname, value);
        }

        string ptype;
        public string pType
        {
            get => ptype;
            set => SetProperty(ref ptype, value);
        }
        string pbirth;
        public string pBirth
        {
            get => pbirth;
            set => SetProperty(ref pbirth, value);
        }
        string ppassport;
        public string pPassport
        {
            get => ppassport;
            set => SetProperty(ref ppassport, value);
        }

        void PushDataGridVisibility()
        {
            if (Finded.Count > 0)
            {
                PushDataGrid = true;
            }
            else PushDataGrid = false;
        }

        // Visibility

        bool pushDataGrid = false;
        public bool PushDataGrid
        {
            get => pushDataGrid;
            set => SetProperty(ref pushDataGrid, value);
        }

    }
}

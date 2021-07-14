using Animal_Clinic_Administration.Entity;
using Animal_Clinic_Administration.Models;
using Animal_Clinic_Administration.X;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using RelayCommand = GalaSoft.MvvmLight.CommandWpf.RelayCommand;

namespace Animal_Clinic_Administration.ViewModels
{
    class MainViewModel : Base
    {
        public MainViewModel()
        {
            InitCommands();
            CheckSelectedMethod();
        }
        private void InitCommands()
        {
            OpenEditMenuCommand = new RelayCommand(ExecuteOpenEditMenu, CanOpenEditMenu);
            OpenRegistrationMenuCommand = new RelayCommand(ExecuteRegistrationMenu);
            OpenSearchMenuCommand = new RelayCommand(ExecuteSearchMenu,CanOpenSearchMenu);
            SelectedPersonCommand = new RelayCommand(ExecuteSelectedPerson, CanOpenInfoSelectedPerson);
            SelectedPetCommand = new RelayCommand(ExecuteSelectedPet, CanOpenInfoSelectedPet);


        }
        AddViewModel addViewModel;
        public AddViewModel AddViewModel
        {
            get => addViewModel;
            set => SetProperty(ref addViewModel, value);
        }
        EditViewModel editViewModel;
        public EditViewModel EditViewModel
        {
            get => editViewModel;
            set => SetProperty(ref editViewModel, value);
        }

        // Selected       
        MyPet seletedPet;
        public MyPet SelectedPet
        {
            get => seletedPet;
            set { SetProperty(ref seletedPet, value); GetSelectedPerson(); }
        }

        MyPerson selectedPerson;
        public MyPerson SelectedPerson
        {
            get => selectedPerson;
            set => SetProperty(ref selectedPerson, value);
        }


        // For CurrentPerson

        public MyPerson currentPerson;
        public MyPerson CurrentPerson
        {
            get => currentPerson;

            set => SetProperty(ref currentPerson, value);
        }


        Person editPerson;
        public Person EditPerson
        {
            get => editPerson;
            set => SetProperty(ref editPerson, value);
        }

        //Strings for textboxes

        // Main Window Search

        string t1;
        public string T1
        {
            get => t1;
            set => SetProperty(ref t1, value);
        }
        public string T1first
        {
            get => T1[0].ToString();
        }
        
        string t2;
        public string T2
        {
            get => t2;
            set => SetProperty(ref t2, value);
        }
        public string T2first
        {
            get => T2[0].ToString();
        }


        string t3;
        public string T3
        {
            get => t3;
            set => SetProperty(ref t3, value);
        }
        
        public DateTime T3asDate
        {
            get
            {
                if (StaticFunctions.DateChecker(T3))
                {
                    return Convert.ToDateTime(T3);
                }
                return DateTime.MinValue;
            }
        }
        public int T3Converted
        {
            get => (int)Convert.ToInt32(T3);
        }

        string t4;
        public string T4
        {
            get => t4;
            set => SetProperty(ref t4, value);
        }
        public string T4First
        {
            get => T4[0].ToString();
        }

        string t5;
        public string T5
        {
            get => t5;
            set => SetProperty(ref t5, value);
        }
        public string T5First
        {
            get => T5[0].ToString();

        }
        // Strings for Labels


        string r1;
        public string R1
        {
            get => r1;
            set => SetProperty(ref r1, value);
        }

        string r2;
        public string R2
        {
            get => r2;
            set => SetProperty(ref r2, value);
        }

        string r3;
        public string R3
        {
            get => r3;
            set => SetProperty(ref r3, value);
        }

        string r4;
        public string R4
        {
            get => r4;
            set => SetProperty(ref r4, value);
        }

        string r5;
        public string R5
        {
            get => r5;
            set => SetProperty(ref r5, value);
        }

        // View Profile Strings


        string vfirstName;
        public string vFirstName
        {
            get => vfirstName;
            set => SetProperty(ref vfirstName, value);
        }


        string vlastName;
        public string vLastName
        {
            get => vlastName;
            set => SetProperty(ref vlastName, value);
        }


        string vage;
        public string vAge
        {
            get => vage;
            set => SetProperty(ref vage, value);
        }


        string vphone;
        public string vPhone
        {
            get => vphone;
            set => SetProperty(ref vphone, value);
        }


        string vmail;
        public string vMail
        {
            get => vmail;
            set => SetProperty(ref vmail, value);
        }

        // Change Search Criteries

        bool selectedMethod = true;
        public bool SelectedMethod
        {
            get => selectedMethod;
            set => SetProperty(ref selectedMethod, value);
        }

        // Commands
        public ICommand SelectedPersonCommand { get; private set; }

        private void ExecuteSelectedPerson()
        {
            SelectedMethod = true;
            CheckSelectedMethod();
            ClearSearchStrings();

        }

        private bool CanOpenInfoSelectedPerson()
        {
            if (S_RegisterVis || S_DataGridVis) return false;
            return true;
        }
        public ICommand SelectedPetCommand { get; private set; }

        private void ExecuteSelectedPet()
        {
            SelectedMethod = false;
            CheckSelectedMethod();
            ClearSearchStrings();
        }

        private bool CanOpenInfoSelectedPet()
        {
            if (S_RegisterVis || S_DataGridVis) return false;
            return true;
        }



        public ICommand OpenEditMenuCommand { get; private set; }
        private void ExecuteOpenEditMenu()
        {
            EditViewModel = new EditViewModel(this);
            ProfileVis = true;
            DataGrid1Vis = false;
            DataGrid0Vis = false;
        }
        private bool CanOpenEditMenu()
        {
            return selectedPerson != null;
        }
        public ICommand OpenRegistrationMenuCommand { get; private set; }

        private void ExecuteRegistrationMenu()
        {
            AddViewModel = new AddViewModel(this);
            S_RegisterVis = true;
            DataGrid1Vis = false;
            DataGrid0Vis = false;

        }
        public ICommand OpenSearchMenuCommand { get; private set; }

        private void ExecuteSearchMenu()
        {
            Db_GetData();
        }

        private bool CanOpenSearchMenu()
        {
            if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5)) return false;
            return true;
        }

        
        public ICommand SelectionChange { get; private set; }

        // Functions  
        void ClearSearchStrings()
        {
            T1 = "";
            T2 = "";
            T3 = "";
            T4 = "";
            T5 = "";

        }
        void CheckSelectedMethod()
        {
            if (SelectedMethod == true)
            {
                R1 = "First Name";
                R2 = "Last Name";
                R3 = "Age";
                R4 = "Phone";
                R5 = "Mail";
                DataGrid0Vis = false;
                DataGrid1Vis = true;
            }
            else
            {
                R1 = "Name";
                R2 = "Type";
                R3 = "Birth";
                R4 = "Passport";
                DataGrid0Vis = true;
                DataGrid1Vis = false;
            }
        }

        public void GetSelectedPerson()
        {
            if (seletedPet != null)
            {
                SelectedPerson = PersonCollection.Where(p => p.Id == SelectedPet.Per_Id).First();
            }
        }

        // Visibility Control

        bool profileVis = false;
        public bool ProfileVis
        {
            get => profileVis;
            set => SetProperty(ref profileVis, value);
        }

        bool dataGrid0Vis;
        public bool DataGrid0Vis
        {
            get => dataGrid0Vis;
            set => SetProperty(ref dataGrid0Vis, value);
        }

        bool datagrid1Vis;
        public bool DataGrid1Vis
        {
            get => datagrid1Vis;
            set => SetProperty(ref datagrid1Vis, value);
        }

        bool confirmvis;
        public bool ConfirmVis
        {
            get => confirmvis;
            set => SetProperty(ref confirmvis, value);
        }

        bool s_RegisterVis = false;
        public bool S_RegisterVis
        {
            get => s_RegisterVis;
            set => SetProperty(ref s_RegisterVis, value);
        }

        bool s_DataGridVis;
        public bool S_DataGridVis
        {
            get => s_DataGridVis;
            set => SetProperty(ref s_DataGridVis, value);
        }


        //  Collections

        ObservableCollection<MyPerson> personCollection;
        public ObservableCollection<MyPerson> PersonCollection
        {
            get => personCollection;
            set => SetProperty(ref personCollection, value);
        }

        ObservableCollection<MyPet> petCollection;
        public ObservableCollection<MyPet> PetCollection
        {
            get => petCollection;
            set => SetProperty(ref petCollection, value);
        }

        public void Db_GetData()
        {
            using (my_clinicEntities db = new my_clinicEntities())
            {
                if (SelectedMethod == true)
                {
                    if (!string.IsNullOrEmpty(T3) && StaticFunctions.IsAge(T3) == false) return;
                    var res = new List<Person>();
                    
                    if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) && p.first_name.ToUpper().StartsWith(T1first.ToUpper()) &&
                        p.last_name.ToUpper().Contains(T2.ToUpper()) && p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.age == T3Converted &&
                        p.phone.Contains(T4) && p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();

                    }
                    
                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) && p.first_name.ToUpper().StartsWith(T1first.ToUpper()) &&

                        p.last_name.ToUpper().Contains(T2.ToUpper()) && p.last_name.ToUpper().StartsWith(T2first.ToUpper()) &&

                        p.age == T3Converted && p.phone.Contains(T4) && p.phone.StartsWith(T4First)).ToList();


                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) && p.first_name.ToUpper().StartsWith(T1first.ToUpper()) &&

                        p.last_name.ToUpper().Contains(T2.ToUpper()) && p.last_name.ToUpper().StartsWith(T2first.ToUpper()) &&

                        p.age == T3Converted && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                        p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.last_name.ToUpper().Contains(T2.ToUpper())

                        && p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.phone.Contains(T4) && p.phone.StartsWith(T4First) &&
                       p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }
                    
                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                        p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.age == T3Converted &&

                        p.phone.Contains(T4) && p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.age == T3Converted &&
                        p.phone.Contains(T4) && p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.age == T3Converted).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.age == T3Converted && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First)).ToList();
                    }
                    
                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.age == T3Converted && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.age == T3Converted && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.age == T3Converted && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.age == T3Converted && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.last_name.ToUpper().Contains(T2.ToUpper()) &&
                        
                         p.last_name.ToUpper().StartsWith(T2first.ToUpper())).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.age == T3Converted).ToList();


                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.age == T3Converted).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.phone.Contains(T4) &&

                        p.phone.StartsWith(T4First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.phone.Contains(T4) &&

                         p.phone.StartsWith(T4First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper()) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first.ToUpper()) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.phone.Contains(T4) && p.phone.StartsWith(T4First) && p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.first_name.ToUpper().Contains(T1.ToUpper()) &&

                         p.first_name.ToUpper().StartsWith(T1first.ToUpper())).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.last_name.ToUpper().Contains(T2.ToUpper()) &&

                        p.last_name.ToUpper().StartsWith(T2first)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.age == T3Converted).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4) && string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.phone.Contains(T4) && p.phone.StartsWith(T4First)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4) && !string.IsNullOrEmpty(T5))
                    {
                        res = db.Person.Where(p => p.mail.Contains(T5) && p.mail.StartsWith(T5First)).ToList();
                    }

                    PersonCollection = new ObservableCollection<MyPerson>();
                    foreach (var x in res)
                    {
                        PersonCollection.Add(new MyPerson()
                        {

                            FirstName = x.first_name,
                            LastName = x.last_name,
                            Age = x.age,
                            Phone = x.phone,
                            Mail = x.mail,
                            Id = x.Id,
                            Pet = StaticFunctions.conver(x.Pet)

                        });
                    }
                }

                else
                {
                    var res = new List<Person>();
                    if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                         x.name.ToUpper().StartsWith(T1first) && x.type.ToUpper().Contains(T2) &&

                         x.type.ToUpper().StartsWith(T2first) && x.birth == T3asDate && x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();

                    }
                    
                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                        x.name.ToUpper().StartsWith(T1first) && x.type.ToUpper().Contains(T2) &&

                        x.type.ToUpper().StartsWith(T2first) && x.birth == T3asDate)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                        x.name.ToUpper().StartsWith(T1first) && x.type.ToUpper().Contains(T2) &&

                        x.type.ToUpper().StartsWith(T2first) && x.passport_number.Contains(T4) &&

                        x.passport_number.StartsWith(T4First))).ToList();
                    }
                    
                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                         x.name.ToUpper().StartsWith(T1first) && x.birth == T3asDate && x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.type.ToUpper().Contains(T2) &&

                         x.type.ToUpper().StartsWith(T2first) && x.birth == T3asDate && x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                         x.name.ToUpper().StartsWith(T1first) && x.type.ToUpper().Contains(T2) &&

                         x.type.ToUpper().StartsWith(T2first))).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                         x.name.ToUpper().StartsWith(T1first) && x.birth == T3asDate)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.type.ToUpper().Contains(T2) &&

                         x.type.ToUpper().StartsWith(T2first) && x.birth == T3asDate)).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                         x.name.ToUpper().StartsWith(T1first) && x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.type.ToUpper().Contains(T2) &&

                         x.type.ToUpper().StartsWith(T2first) && x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.birth == T3asDate && x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();
                    }

                    else if (!string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.name.ToUpper().Contains(T1.ToUpper()) &&

                         x.name.ToUpper().StartsWith(T1first))).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && !string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.type.ToUpper().Contains(T2) &&

                         x.type.ToUpper().StartsWith(T2first))).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && !string.IsNullOrEmpty(T3) && string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.birth == T3asDate)).ToList();
                    }

                    else if (string.IsNullOrEmpty(T1) && string.IsNullOrEmpty(T2) && string.IsNullOrEmpty(T3) && !string.IsNullOrEmpty(T4))
                    {
                        res = db.Person.Where(p => p.Pet.All(x => x.passport_number.Contains(T4) &&

                         x.passport_number.StartsWith(T4First))).ToList();
                    }

                    PersonCollection = new ObservableCollection<MyPerson>();
                    foreach (var x in res)
                    {
                        PersonCollection.Add(new MyPerson()
                        {

                            FirstName = x.first_name,
                            LastName = x.last_name,
                            Age = x.age,
                            Phone = x.phone,
                            Mail = x.mail,
                            Id = x.Id,
                            Pet = StaticFunctions.conver(x.Pet)

                        });
                    }
                    
                    var res2 = from per in PersonCollection from p in per.Pet where p.Per_Id == per.Id select p;
                    PetCollection = new ObservableCollection<MyPet>();
                    foreach (var r in res2)
                    {
                        PetCollection.Add(new MyPet()
                        {
                            Name = r.Name,
                            Type = r.Type,
                            Birth = r.Birth.ToString(),
                            MyDisease = r.MyDisease,
                            MyVaccination = r.MyVaccination,
                            PassportNumber = r.PassportNumber,
                            Per_Id = r.Per_Id,
                            P_Id = r.P_Id
                        });
                    }
                }
            }
        }
    }
}


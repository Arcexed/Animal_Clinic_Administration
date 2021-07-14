using Animal_Clinic_Administration.Entity;
using Animal_Clinic_Administration.Models;
using Animal_Clinic_Administration.X;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;

namespace Animal_Clinic_Administration.ViewModels
{
    class EditViewModel : Base
    {
        private void InitCommands()
        {
            ActionChangedCommand = new RelayCommand(ExecuteActionChanged);
            ExecuteEditCommand = new RelayCommand(ExecuteEdit);
            CloseProfileCommand = new RelayCommand(ExecuteCloseProfile);
        }

        public ICommand ActionChangedCommand { get; private set; }

        public void ExecuteActionChanged()
        {
            switch (SelectedAction)
            {
                case "Edit Person":
                    EditPerson();
                    break;

                case "Add Vaccination":
                    AddVaccination();
                    break;

                case "Add Disease":
                    AddDisease();
                    break;

                case "Edit Vaccination":
                    EditVaccination();
                    break;

                case "Edit Disease":
                    EditDisease();
                    break;

                case "Edit Pet":
                    EditPet();
                    break;

                case "Delete Person":
                    ATextClear();
                    ATextHide();
                    break;


                case "Delete Pet":
                    ATextClear();
                    ATextHide();
                    break;

                case "Delete Vaccination":
                    ATextClear();
                    ATextHide();
                    break;

                case "Delete Disease":
                    ATextClear();
                    ATextHide();
                    break;
            }
        }

        public EditViewModel(MainViewModel obj)
        {
            mainVM = obj;
            SelectedAction = cms.First();
            InitCommands();
            ActionChangedCommand.Execute(obj);
        }
        MainViewModel mainVM;
        public MainViewModel MainVM
        {
            get => mainVM;
            set { mainVM = value; OnPropertyChanged(); }
        }
        MyDiseases selectedDisease;
        public MyDiseases SelectedDisease
        {
            get => selectedDisease;
            set => SetProperty(ref selectedDisease, value);
        }
        MyVaccinations selectedVaccination;
        public MyVaccinations SelectedVaccination
        {
            get => selectedVaccination;
            set => SetProperty(ref selectedVaccination, value);
        }

        int selectedEdit;
        int SelectedEdit
        {
            get => selectedEdit;
            set => SetProperty(ref selectedEdit, value);
        }
        //visibility

        public bool DatVis
        {
            get => MainVM.DataGrid1Vis;
            set { MainVM.DataGrid1Vis = value; OnPropertyChanged(); }
        }
        public bool ProfVis
        {
            get => MainVM.ProfileVis;
            set { MainVM.ProfileVis = value; OnPropertyChanged(); }
        }

        bool e1vis;
        public bool E1vis
        {
            get => e1vis;
            set => SetProperty(ref e1vis, value);
        }

        bool e2vis;
        public bool E2vis
        {
            get => e2vis;
            set => SetProperty(ref e2vis, value);
        }

        bool e3vis;
        public bool E3vis
        {
            get => e3vis;
            set => SetProperty(ref e3vis, value);
        }

        bool e4vis;
        public bool E4vis
        {
            get => e4vis;
            set => SetProperty(ref e4vis, value);
        }

        bool e5vis;
        public bool E5vis
        {
            get => e5vis;
            set => SetProperty(ref e5vis, value);
        }

        // Edit TextBox 
        string et1;
        public string Et1
        {
            get => et1;
            set => SetProperty(ref et1, value);
        }

        string et2;
        public string Et2
        {
            get => et2;
            set => SetProperty(ref et2, value);
        }

        string et3;
        public string Et3
        {
            get => et3;
            set => SetProperty(ref et3, value);
        }

        string et4;
        public string Et4
        {
            get => et4;
            set => SetProperty(ref et4, value);
        }

        string et5;
        public string Et5
        {
            get => et5;
            set => SetProperty(ref et5, value);
        }

        MyPet selectedPetForEdit;
        public MyPet SelectedPetForEdit
        {
            get => selectedPetForEdit;
            set => SetProperty(ref selectedPetForEdit, value);
        }

        // Edit Labels


        // Edit
        string e1;
        public string E1
        {
            get => e1;
            set => SetProperty(ref e1, value);
        }

        string e2;
        public string E2
        {
            get => e2;
            set => SetProperty(ref e2, value);
        }

        string e3;
        public string E3
        {
            get => e3;
            set => SetProperty(ref e3, value);
        }

        string e4;
        public string E4
        {
            get => e4;
            set => SetProperty(ref e4, value);
        }

        string e5;
        public string E5
        {
            get => e5;
            set => SetProperty(ref e5, value);
        }

        public static string[] cms { 
            get; 
        } = {
            "Edit Person", "Edit Pet", "Edit Disease","Edit Vaccination",

            "Add Vaccination","Add Disease","Delete Person", "Delete Pet", "Delete Vaccination",

            "Delete Disease"
        };

        string selectedAction;
        public string SelectedAction
        {
            get => selectedAction;
            set => SetProperty(ref selectedAction, value);
        }
        //Commands

        public ICommand ExecuteEditCommand { get; private set; }

        private void ExecuteEdit()
        {
            int Xid = MainVM.SelectedPerson.Id;
            bool deleted = false;
            using (my_clinicEntities db = new my_clinicEntities())
            {
                switch (SelectedAction)
                {
                    case "Delete Person":
                        if (MainVM.SelectedPerson == null)
                        {
                            MessageBox.Show("Please select Person");
                            CloseProfileCommand.Execute(null);
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var res = db.Person.Where(p => p.Id == MainVM.SelectedPerson.Id).First();
                                    db.Person.Remove(res);
                                    db.SaveChanges();
                                    transaction.Commit();
                                    deleted = true;
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;

                    case "Delete Pet":
                        if (SelectedPetForEdit == null)
                        {
                            MessageBox.Show("Please select Pet");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var res = db.Pet.Where(p => p.P_Id == SelectedPetForEdit.P_Id).First();

                                    db.Pet.Remove(res);
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;

                    case "Delete Vaccination":
                        if (SelectedVaccination == null)
                        {
                            MessageBox.Show("Please select Vaccination");
                            break;
                        }
                        else
                        {

                            using (var transaction = db.Database.BeginTransaction())
                            {

                                try
                                {
                                    var res = db.Vaccination.Where(v => v.V_Id == SelectedVaccination.V_Id).First();
                                    db.Vaccination.Remove(res);
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }

                        break;



                    case "Delete Disease":
                        if (SelectedDisease == null)
                        {
                            MessageBox.Show("Please select Disease");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {

                                try
                                {
                                    var res = db.Diseases.Where(d => d.D_Id == SelectedDisease.D_Id).First();
                                    db.Diseases.Remove(res);
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;



                    case "Edit Person":


                        if (string.IsNullOrEmpty(Et1) || string.IsNullOrEmpty(Et2) || string.IsNullOrEmpty(Et4))
                        {
                            MessageBox.Show("First Name , Last Name , Phone cannot be empty");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var x = db.Person.Where(p => p.Id == MainVM.SelectedPerson.Id).First();
                                    x.first_name = Et1;
                                    x.last_name = Et2;
                                    x.age = (Nullable<int>)Convert.ToInt32(Et3);
                                    x.phone = Et4;
                                    x.mail = Et5;
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;


                    case "Edit Pet":
                        if (SelectedPetForEdit == null)
                        {
                            MessageBox.Show("Select Pet");
                            break;
                        }
                        else if (string.IsNullOrEmpty(Et1) || string.IsNullOrEmpty(Et2))
                        {
                            MessageBox.Show("Name , Type cannot be empty");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var y = db.Pet.Where(c => c.P_Id == selectedPetForEdit.P_Id).First();
                                    y.name = Et1;
                                    y.type = Et2;
                                    if (Et3.Length > 0) y.birth = Convert.ToDateTime(Et3);
                                    y.passport_number = Et4;
                                    db.SaveChanges();
                                    transaction.Commit();

                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;

                    case "Edit Disease":
                        if (string.IsNullOrEmpty(Et1))
                        {
                            MessageBox.Show("Diagnosis cannot be empty");
                            break;

                        }
                        else if (SelectedPetForEdit == null)
                        {
                            MessageBox.Show("Please Select Pet");
                            break;
                        }
                        else if (SelectedDisease == null)
                        {
                            MessageBox.Show("Please Select Disease");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var a = db.Diseases.Where(d => d.D_Id == SelectedDisease.D_Id).FirstOrDefault();
                                    a.diagnosis = Et1;
                                    a.cured = Et2;
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;


                    case "Edit Vaccination":
                        if (string.IsNullOrEmpty(Et1) || string.IsNullOrEmpty(Et2))
                        {
                            MessageBox.Show("Drug , Vaccination Date cannot be empty");
                            break;

                        }
                        else if (!StaticFunctions.DateChecker(Et2))
                        {
                            MessageBox.Show("Please enter valid Date-Time");
                            break;

                        }
                        else if (SelectedPetForEdit == null)
                        {
                            MessageBox.Show("Please Select Pet");
                            break;
                        }
                        else if (SelectedVaccination == null)
                        {
                            MessageBox.Show("Please Select Vaccination");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {

                                try
                                {
                                    var b = db.Vaccination.Where(v => v.V_Id == SelectedVaccination.V_Id).FirstOrDefault();
                                    b.drug = Et1;
                                    b.v_date = Convert.ToDateTime(Et2);
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }

                        break;


                    case "Add Disease":
                        if (string.IsNullOrEmpty(Et1))
                        {
                            MessageBox.Show("Diagnosis cannot be empty");
                            break;

                        }
                        else if (SelectedPetForEdit == null)
                        {
                            MessageBox.Show("Please Select Pet");
                            break;
                        }
                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var lastdisease = db.Diseases.OrderByDescending(d => d.D_Id).First().D_Id;
                                    Diseases disease = new Diseases()
                                    {
                                        D_Id = lastdisease,
                                        diagnosis = Et1,
                                        cured = Et2,
                                        P_Id = SelectedPetForEdit.P_Id

                                    };
                                    db.Diseases.Add(disease);

                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;

                    case "Add Vaccination":
                        if (string.IsNullOrEmpty(Et1) || string.IsNullOrEmpty(Et2))
                        {
                            MessageBox.Show("Drug , Vaccination Date cannot be empty");
                            break;

                        }

                        else if (!StaticFunctions.DateChecker(Et2))
                        {
                            MessageBox.Show("Please enter valid Date-Time");
                            break;
                        }

                        else if (SelectedPetForEdit == null)
                        {
                            MessageBox.Show("Please Select Pet");
                            break;
                        }

                        else
                        {
                            using (var transaction = db.Database.BeginTransaction())
                            {
                                try
                                {
                                    var lastVaccintaion = db.Vaccination.OrderByDescending(v => v.V_Id).First().V_Id;
                                    Vaccination vaccination = new Vaccination()
                                    {
                                        V_Id = lastVaccintaion,
                                        drug = Et1,
                                        v_date = Convert.ToDateTime(Et2),
                                        P_Id = SelectedPetForEdit.P_Id
                                    };
                                    db.Vaccination.Add(vaccination);
                                    db.SaveChanges();
                                    transaction.Commit();
                                }
                                catch (Exception Ex)
                                {
                                    transaction.Rollback();
                                    MessageBox.Show(Ex.Message);
                                }
                            }
                        }
                        break;
                }

                ATextClear();

                MainVM.Db_GetData();

                if (!deleted)
                {
                    MainVM.SelectedPerson = MainVM.PersonCollection.Where(p => p.Id == Xid).First();
                }
                else
                    CloseProfileCommand.Execute(null);
            }
        }

       
        public ICommand CloseProfileCommand { get; private set; }

        private void ExecuteCloseProfile()
        {
            ProfVis = false;
            DatVis = true;
            E1vis = true;
            E2vis = true;
            E3vis = false;
            E4vis = false;
            E5vis = false;
        }



        //Functions  


        void EditPerson()
        {

            E1vis = true;
            E2vis = true;
            E3vis = true;
            E4vis = true;
            E5vis = true;
            E1 = "First name";
            E2 = "Last Name";
            E3 = "Age";
            E4 = "Phone";
            E5 = "E-Mail";
        }




        void EditPet()
        {

            E1vis = true;
            E2vis = true;
            E3vis = true;
            E4vis = true;
            E1 = "Pet name";
            E2 = "Type";
            E3 = "Birth";
            E4 = "Passport";
            E5vis = false;
        }


        void EditDisease()
        {

            E1 = "Diagnosis";
            E2 = "Cured";
            E1vis = true;
            E2vis = true;
            E3vis = false;
            E4vis = false;
            E5vis = false;
        }

        void EditVaccination()
        {

            E1 = "Drug";
            E2 = "Date";
            E1vis = true;
            E2vis = true;
            E3vis = false;
            E4vis = false;
            E5vis = false;
        }




        void AddDisease()
        {

            E1 = "Diagnosis";
            E2 = "Cured";
            E1vis = true;
            E2vis = true;
            E3vis = false;
            E4vis = false;
            E5vis = false;
        }


        void AddVaccination()
        {

            E1 = "Drug";
            E2 = "Date";
            E1vis = true;
            E2vis = true;
            E3vis = false;
            E4vis = false;
            E5vis = false;
        }

        void ATextClear()
        {
            Et1 = "";
            Et2 = "";
            Et3 = "";
            Et4 = "";
            Et5 = "";

        }

        void ATextHide()
        {
            E1vis = false;
            E2vis = false;
            E3vis = false;
            E4vis = false;
            E5vis = false;


        }

    }
}

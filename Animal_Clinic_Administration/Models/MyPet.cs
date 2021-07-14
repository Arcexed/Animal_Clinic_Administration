using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animal_Clinic_Administration.Entity;
using Animal_Clinic_Administration.ViewModels;

namespace Animal_Clinic_Administration.Models
{
    class MyPet : Base
    { 
        int p_id;
        public int P_Id
        {
            get => p_id;
            set => SetProperty(ref p_id, value);
        }

        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }

        string birth;
        public string Birth
        {
            get => birth;
            set => SetProperty(ref birth, value);
        }

        int per_id;
        public int Per_Id
        {
            get => per_id;
            set => SetProperty(ref per_id, value);
        }

        string passportNumber;
        public string PassportNumber
        {
            get => passportNumber;
            set => SetProperty(ref passportNumber, value);
        } 
        public ObservableCollection<MyVaccinations> MyVaccination { get; set; } 
        public ObservableCollection<MyDiseases> MyDisease { get; set; } 
    }
}

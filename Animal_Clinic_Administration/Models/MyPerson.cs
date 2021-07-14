using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animal_Clinic_Administration.ViewModels;

namespace Animal_Clinic_Administration.Models
{
    class MyPerson : Base
    {
        int id;
        public int Id
        {
            get => id;
            set => SetProperty(ref id, value);
        }

        string firstName;
        public string FirstName
        {
            get => firstName;
            set => SetProperty(ref firstName, value);
        }

        string lastName;
        public string LastName
        {
            get => lastName;
            set => SetProperty(ref lastName, value);
        }

        int? age;
        public int? Age
        {
            get => age;
            set => SetProperty(ref age, value);
        }

        string phone;
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        string mail;
        public string Mail
        {
            get => mail;
            set => SetProperty(ref mail, value);
        }

        public ObservableCollection<MyPet> Pet { get; set; }
    }



}

using Animal_Clinic_Administration.Entity;
using Animal_Clinic_Administration.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Animal_Clinic_Administration
{
    static class StaticFunctions
    {
        public static bool DateChecker(string v)
        {
            try
            {
                if (Convert.ToDateTime(v).GetType() == typeof(DateTime)) return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
        public static bool IsAge(string str)
        {
            int a = 0;

            if (int.TryParse(str, out a))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Please enter valid Age");
                return false;
            }
        }
        public static ObservableCollection<MyPet> conver(IEnumerable<Pet> pets)
        {
            ObservableCollection<MyPet> res = new ObservableCollection<MyPet>();
            foreach (var x in pets)
            {
                res.Add(new MyPet()
                {
                    Name = x.name,
                    Birth = x.birth.ToString(),
                    Per_Id = (int)x.Per_Id,
                    P_Id = x.P_Id,
                    Type = x.type,
                    PassportNumber = x.passport_number,
                    MyVaccination = conver2(x.Vaccination),
                    MyDisease = conver3(x.Diseases)

                });
            }
            return res;
        }
        public static ObservableCollection<MyVaccinations> conver2(IEnumerable<Vaccination> vcs)
        {
            ObservableCollection<MyVaccinations> v = new ObservableCollection<MyVaccinations>();

            foreach (var x in vcs)
            {
                v.Add(new MyVaccinations()
                {
                    V_date = x.v_date.ToString(),
                    P_Id = x.P_Id,
                    Drug = x.drug,
                    V_Id = x.V_Id


                });
            }
            return v;
        }
        public static ObservableCollection<MyDiseases> conver3(IEnumerable<Diseases> dis)
        {
            ObservableCollection<MyDiseases> d = new ObservableCollection<MyDiseases>();

            foreach (var x in dis)
            {
                d.Add(new MyDiseases()
                {
                    Cured = x.cured,
                    Diagnose = x.diagnosis,
                    P_Id = x.P_Id,
                    D_Id = x.D_Id

                });
            }
            return d;
        }
        public static ObservableCollection<MyPerson> conver4(List<Person> prs)
        {
            ObservableCollection<MyPerson> p = new ObservableCollection<MyPerson>();

            foreach (var x in prs)
            {
                p.Add(new MyPerson()
                {
                    FirstName = x.first_name,
                    LastName = x.last_name,
                    Age = x.age,
                    Phone = x.phone,
                    Mail = x.mail,
                    Id = x.Id,
                    Pet = conver(x.Pet)

                });
            }
            return p;
        }

        public static MyPerson PersonToMyPerson(Person p)
        {
            return new MyPerson()
            {
                FirstName = p.first_name,
                Phone = p.phone,
                Mail = p.mail,
                Age = p.age,
                LastName = p.last_name,
                Id = p.Id,
                Pet = StaticFunctions.conver(p.Pet)
            };

        }
        public static bool ValidTelephone(string telNo)
        {
            return Regex.Match(telNo, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$").Success;
        }
    }
}

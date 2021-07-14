using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animal_Clinic_Administration.ViewModels;


namespace Animal_Clinic_Administration.Models
{
    class MyVaccinations : Base
    {
        int v_id;
        public int V_Id
        {
            get => v_id;
            set => SetProperty(ref v_id, value);
        } 
        string drug;
        public string Drug
        {
            get => drug;
            set => SetProperty(ref drug, value);
        }
        string v_date;
        public string V_date
        {
            get => v_date;
            set => SetProperty(ref v_date, value);
        }
        int? p_id;
        public int? P_Id
        {
            get => p_id;
            set => SetProperty(ref p_id, value);
        }
    }
}

using Animal_Clinic_Administration.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animal_Clinic_Administration.Models
{
    class MyDiseases : Base
    {
        int d_id;
        public int D_Id
        {
            get => d_id;
            set => SetProperty(ref d_id, value);
        }

        string diagnose;
        public string Diagnose
        {
            get => diagnose;
            set => SetProperty(ref diagnose, value);
        }

        string cured;

        public string Cured
        {
            get => cured;
            set => SetProperty(ref cured, value);
        }

        int? p_id;
        public int? P_Id
        {
            get => p_id;
            set => SetProperty(ref p_id, value);
        }

    }
}

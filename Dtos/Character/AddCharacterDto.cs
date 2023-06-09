using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_YU.Dtos.Character
{
    public class AddCharacterDto
    {
        
        public string Name {get;set;} = "Carlos el mago";

        public int Hitpoints {get;set;} = 100;

        public int Strength {get;set;} = 10;

        public int Defense {get;set;} = 10;

        public int Intelligence {get;set;} = 10;

        public RpgClass Class {get; set;} = RpgClass.Mage;
    }
    
}
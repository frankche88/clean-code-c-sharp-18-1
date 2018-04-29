
using System.Collections.Generic;
namespace BusinessLayer
{
    /// <summary>
    /// Represents a single conference session
    /// </summary>
    public class Session
	{
    	readonly List<string> programLanguage = new List<string>() { "Cobol", "Punch Cards", "Commodore", "VBScript" };
    	
    	private bool approved = true;
    	
		public string Title { get; set; }
		public string Description { get; set; }
		
		public bool Approved 
		{
	      get 
	      {
	         return approved;  
	      }  
	   }
		

		public Session(string title, string description)
		{
			Title = title;
			Description = description;
			
			if(title != null && description != null) {
				foreach (var tech in programLanguage)
				{
					if(title.Contains(tech) || description.Contains(tech)) 
					{
						approved = false;
						break;
					}
				}
			}
		}
	}
}

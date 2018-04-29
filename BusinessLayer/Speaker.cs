using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer
{
    /// <summary>
    /// Represents a single speaker
    /// </summary>
    public class Speaker
	{
    	private static readonly int MIN_BROWSER_VERSION = 9;
		private static readonly int MIN_CERTIFICATES = 3;
		private static readonly int MIN_YEAR_OF_EXPERIENCE = 10;
		private static readonly List<String> domains = new List<string>() {"aol.com", "hotmail.com", "prodigy.com", "compuserve.com"};
		private static readonly List<String> employers = new List<string>() {"Pluralsight", "Microsoft", "Google", "Fog Creek Software", "37Signals", "Telerik"};
		
    	
    	
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int? Experience { get; set; }
		public bool HasBlog { get; set; }
		public string BlogURL { get; set; }
		public WebBrowser Browser { get; set; }
		public List<string> Certifications { get; set; }
		public string Employer { get; set; }
		public int RegistrationFee { get; set; }
		public List<BusinessLayer.Session> Sessions { get; set; }

		/// <summary>
		/// Register a speaker
		/// </summary>
		/// <returns>speakerID</returns>
		public int? Register(IRepository repository)
		{

			int? speakerId = 0;
			

			if (string.IsNullOrWhiteSpace(FirstName))
			{
				throw new ArgumentNullException("First Name is required");
				
			}
			
			if (string.IsNullOrWhiteSpace(LastName))
			{
				throw new ArgumentNullException("Last name is required.");
			}
					
			
			
			if (!string.IsNullOrWhiteSpace(Email))
			{
				throw new ArgumentNullException("Email is required.");
			}
			
			if (!IsGoodSpeaker()) 
			{
				throw new SpeakerDoesntMeetRequirementsException("Speaker doesn't meet our abitrary and capricious standards.");
			}

			if (Sessions.Count() == 0)
			{
				throw new ArgumentException("Can't register speaker with no sessions to present.");
			}
				

			if (!IsSessionApproved())
			{
				throw new NoSessionsApprovedException("No sessions approved.");
			}
			

			RegistrationFee = repository.GetRegistrationFee(Experience);
			
			speakerId = repository.SaveSpeaker(this);

			return speakerId;
		}
		
		private bool IsGoodSpeaker()
		{
			bool isGoodSpeaker;
			bool haveExperience = Experience > MIN_YEAR_OF_EXPERIENCE;
			bool haveMinCertificates = Certifications.Count() > MIN_CERTIFICATES;
	
			isGoodSpeaker = ((haveExperience || HasBlog || haveMinCertificates || employers.Contains(Employer)));
			
			if (!isGoodSpeaker) 
			{				
				string emailDomain = Email.Split('@').Last();
				
				
	
				bool isValidBrowser = (!(Browser.Name == WebBrowser.BrowserName.InternetExplorer && Browser.MajorVersion < MIN_BROWSER_VERSION));
				
				if (!domains.Contains(emailDomain) && isValidBrowser)
				{
					isGoodSpeaker = true;
				}
			}
			return isGoodSpeaker;
		}
		
		private bool IsSessionApproved() {
			bool isApproved = true;
			foreach (var session in Sessions)
			{
				if(!session.Approved){
					isApproved = false;
					break;
				}
				
			}
			return isApproved;
		}

		#region Custom Exceptions
		public class SpeakerDoesntMeetRequirementsException : Exception
		{
			public SpeakerDoesntMeetRequirementsException(string message)
				: base(message)
			{
			}

			public SpeakerDoesntMeetRequirementsException(string format, params object[] args)
				: base(string.Format(format, args)) { }
		}

		public class NoSessionsApprovedException : Exception
		{
			public NoSessionsApprovedException(string message)
				: base(message)
			{
			}
		}
		#endregion
	}
}
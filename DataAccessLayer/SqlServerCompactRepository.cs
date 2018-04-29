using BusinessLayer;

namespace DataAccessLayer
{
    public class SqlServerCompactRepository : IRepository
	{
		public int SaveSpeaker(Speaker speaker)
		{
			//TODO: Save speaker to DB for now. For demo, just assume success and return 1.
			return 1;
		}
		
		public int GetRegistrationFee(int? experience)
		{
			if (experience <= 1)
			{
				return 500;
			}
			
			if (experience >= 2 && experience <= 3)
			{
				return 250;
			}
			if (experience >= 4 && experience <= 5)
			{
				return 100;
			}
			if (experience >= 6 && experience <= 9)
			{
				return 50;
			}
			
			return 0;
			
		}
	}
}

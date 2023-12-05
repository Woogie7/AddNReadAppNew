namespace DAL.Entityes.Base
{
	public abstract class PersonEntity : NameEntity 
	{
		public string? Surnname { get; set; }
		public string? Patronymic {  get; set; }
	}

}

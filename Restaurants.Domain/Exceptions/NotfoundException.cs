namespace Restaurants.Domain.Exceptions
{
	public class NotfoundException(string resourceType , string resourceIndentiyer) : Exception($"{resourceType} with Id: {resourceIndentiyer} does not exist ")
	{

	}
}

namespace Gamer.Microservice.Game.Content
{

	class Hosting
	{
	
		static void Main(string[] args)
		{
		
			Gamer.Access.GameDefinition.Service.Hosting.RegisterService();
			Gamer.Access.GameSession.Service.Hosting.RegisterService();
			Gamer.Access.Player.Service.Hosting.RegisterService();
			Gamer.Access.Tile.Service.Hosting.RegisterService();

			Gamer.Engine.GameBoard.Service.Hosting.RegisterService();
			Gamer.Engine.GamePlay.Service.Hosting.RegisterService();

			Gamer.Manager.Game.Service.Hosting.RegisterService();

		}

	}

}

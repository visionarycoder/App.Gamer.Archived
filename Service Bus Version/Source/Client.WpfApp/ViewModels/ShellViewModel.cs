using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using Gamer.Client.WpfApp.Models;
using Prism.Commands;
using Prism.Mvvm;
using ServiceModelEx;

namespace Gamer.Client.WpfApp.ViewModels
{

	[Export]
	public class ShellViewModel : BindableBase
	{
		private readonly SubscriptionServiceProxy proxy;

		private DelegateCommand startGameCommand;
		public DelegateCommand StartGameCommand => startGameCommand ?? (startGameCommand = new DelegateCommand(HandleStartGameCommand));

		private DelegateCommand<object> playCommand;
		public DelegateCommand<object> PlayCommand => playCommand ?? (playCommand = new DelegateCommand<object>(HandlePlayCommand));

		private Observable<string> a1;
		public Observable<string> A1
		{
			get => a1;
			set => SetProperty(ref a1, value);
		}

		private Observable<string> a2;
		public Observable<string> A2
		{
			get => a2;
			set => SetProperty(ref a2, value);
		}

		private Observable<string> a3;
		public Observable<string> A3
		{
			get => a3;
			set => SetProperty(ref a3, value);
		}

		private Observable<string> b1;
		public Observable<string> B1
		{
			get => b1;
			set => SetProperty(ref b1, value);
		}

		private Observable<string> b2;
		public Observable<string> B2
		{
			get => b2;
			set => SetProperty(ref b2, value);
		}

		private Observable<string> b3;
		public Observable<string> B3
		{
			get => b3;
			set => SetProperty(ref b3, value);
		}

		private Observable<string> c1;
		public Observable<string> C1
		{
			get => c1;
			set => SetProperty(ref c1, value);
		}

		private Observable<string> c2;
		public Observable<string> C2
		{
			get => c2;
			set => SetProperty(ref c2, value);
		}

		private Observable<string> c3;
		public Observable<string> C3
		{
			get => c3;
			set => SetProperty(ref c3, value);
		}

		private bool a1IsEnabled;
		public bool A1IsEnabled
		{
			get => a1IsEnabled;
			set => SetProperty(ref a1IsEnabled, value);
		}

		private bool a2IsEnabled;
		public bool A2IsEnabled
		{
			get => a2IsEnabled;
			set => SetProperty(ref a2IsEnabled, value);
		}

		private bool a3IsEnabled;
		public bool A3IsEnabled
		{
			get => a3IsEnabled;
			set => SetProperty(ref a3IsEnabled, value);
		}

		private bool b1IsEnabled;
		public bool B1IsEnabled
		{
			get => b1IsEnabled;
			set => SetProperty(ref b1IsEnabled, value);
		}

		private bool b2IsEnabled;
		public bool B2IsEnabled
		{
			get => b2IsEnabled;
			set => SetProperty(ref b2IsEnabled, value);
		}

		private bool b3IsEnabled;
		public bool B3IsEnabled
		{
			get => b3IsEnabled;
			set => SetProperty(ref b3IsEnabled, value);
		}

		private bool c1IsEnabled;
		public bool C1IsEnabled
		{
			get => c1IsEnabled;
			set => SetProperty(ref c1IsEnabled, value);
		}

		private bool c2IsEnabled;
		public bool C2IsEnabled
		{
			get => c2IsEnabled;
			set => SetProperty(ref c2IsEnabled, value);
		}

		private bool c3IsEnabled;
		public bool C3IsEnabled
		{
			get => c3IsEnabled;
			set => SetProperty(ref c3IsEnabled, value);
		}

		private ObservableCollection<TraceModel> traceEventCollection;
		public ObservableCollection<TraceModel> TraceEventCollection
		{
			get => traceEventCollection;
			set => SetProperty(ref traceEventCollection, value);
		}

		private string traceMessages;
		public string TraceMessages
		{
			get => traceMessages;
			set => SetProperty(ref traceMessages, value);
		}

		private string gameStatue;
		public string GameStatus
		{
			get => gameStatue;
			set => SetProperty(ref gameStatue, value);
		}

		private string gameMessage;
		public string GameMessage
		{
			get => gameMessage;
			set => SetProperty(ref gameMessage, value);
		}

		private bool isGamePlaying;
		public bool IsGamePlaying
		{
			get => isGamePlaying;
			set => SetProperty(ref isGamePlaying, value);
		}

		private bool isOnePlayerGame;
		public bool IsOnePlayerGame
		{
			get => isOnePlayerGame;
			set
			{
				if (SetProperty(ref isOnePlayerGame, value))
					RaisePropertyChanged(propertyName: nameof(IsTwoPlayerGame));
			}
		}

		public bool IsTwoPlayerGame
		{
			get => !isOnePlayerGame;
			set
			{
				var invert = !value;
				if (SetProperty(ref isOnePlayerGame, invert))
					RaisePropertyChanged(propertyName: nameof(IsOnePlayerGame));
			}
		}

		[ImportingConstructor]
		public ShellViewModel()
		{

			TraceEventCollection = new ObservableCollection<TraceModel>();
			TraceMessages = "";

			var context = new InstanceContext(this);
			proxy = new SubscriptionServiceProxy(context);
			proxy.Subscribe(Constant.EventOperation.START_GAME_RESPONSE);
			proxy.Subscribe(Constant.EventOperation.PLAY_TURN_RESPONSE);
			proxy.Subscribe(Constant.EventOperation.END_GAME_RESPONSE);

			proxy.Subscribe(Constant.EventOperation.TRACE_EVENT);

			A1 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };
			B1 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };
			C1 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };

			A2 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };
			B2 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };
			C2 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };

			A3 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };
			B3 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };
			C3 = new Observable<string> { Value = Constant.TicTacToe.DEFAULT_GAMEPIECE };

			A1IsEnabled = true;
			B1IsEnabled = true;
			C1IsEnabled = true;

			A2IsEnabled = true;
			B2IsEnabled = true;
			C2IsEnabled = true;

			A3IsEnabled = true;
			B3IsEnabled = true;
			C3IsEnabled = true;

			IsOnePlayerGame = true;

		}

		//private void HandleTraceEvent(TraceEventPayload payload)
		//{
		//	Trace.WriteLine($"TRACE: {payload.UtcTimestamp:G} {payload.SourceName} {payload.ActionName}");
		//	var trace = new TraceModel(payload.UtcTimestamp, payload.SourceName, payload.ActionName);
		//	TraceEventCollection.Add(trace);
		//	OnPropertyChanged(() => TraceEventCollection);
		//	TraceMessages += trace + Environment.NewLine;
		//}

		private async void PublishStartGameRequest()
		{

		}

		private async void HandleStartGameCommand()
		{

			var request = new GameCreateRequest
			{
				NumberOfPlayers = IsOnePlayerGame ? 1 : 2
			};

			var gameManager = InProcFactory.CreateInstance<GameManger, IGameManger>();

			var response = await gameManager.StartGame(request);
			foreach (var kvp in response.GamePieces)
				switch (kvp.Key)
				{
					case "A1":
						A1.Value = kvp.Value;
						break;
					case "A2":
						A2.Value = kvp.Value;
						break;
					case "A3":
						A3.Value = kvp.Value;
						break;
					case "B1":
						B1.Value = kvp.Value;
						break;
					case "B2":
						B2.Value = kvp.Value;
						break;
					case "B3":
						B3.Value = kvp.Value;
						break;
					case "C1":
						C1.Value = kvp.Value;
						break;
					case "C2":
						C2.Value = kvp.Value;
						break;
					case "C3":
						C3.Value = kvp.Value;
						break;
					default:
						Trace.WriteLine($"Unable to match key ({kvp.Key}) for {kvp.Value}");
						break;
				}


		}

		//private void HandleTurnRequestEvent(TurnRequestEventPayload payload)
		//{

		//	GameMessage = payload.Message;
		//	GameStatus = payload.Status;
			
		//	UpdateGameBoard(payload.Tiles.ToList());

		//	A1IsEnabled = A1.Value == Constant.DEFAULT_GAMEPIECE;
		//	B1IsEnabled = B1.Value == Constant.DEFAULT_GAMEPIECE;
		//	C1IsEnabled = C1.Value == Constant.DEFAULT_GAMEPIECE;

		//	A2IsEnabled = A2.Value == Constant.DEFAULT_GAMEPIECE;
		//	B2IsEnabled = B2.Value == Constant.DEFAULT_GAMEPIECE;
		//	C2IsEnabled = C2.Value == Constant.DEFAULT_GAMEPIECE;

		//	A3IsEnabled = A3.Value == Constant.DEFAULT_GAMEPIECE;
		//	B3IsEnabled = B3.Value == Constant.DEFAULT_GAMEPIECE;
		//	C3IsEnabled = C3.Value == Constant.DEFAULT_GAMEPIECE;

		//	PlayCommand.CanExecute(true);
		//	PlayCommand.RaiseCanExecuteChanged();

		//}

		private void UpdateGameBoard(List<KeyValuePair<string,string>> tiles)
		{
			A1.Value = tiles.Single(i => i.Key == "A1").Value;
			A2.Value = tiles.Single(i => i.Key == "A2").Value;
			A3.Value = tiles.Single(i => i.Key == "A3").Value;
								 
			B1.Value = tiles.Single(i => i.Key == "B1").Value;
			B2.Value = tiles.Single(i => i.Key == "B2").Value;
			B3.Value = tiles.Single(i => i.Key == "B3").Value;
								 
			C1.Value = tiles.Single(i => i.Key == "C1").Value;
			C2.Value = tiles.Single(i => i.Key == "C2").Value;
			C3.Value = tiles.Single(i => i.Key == "C3").Value;
		}

		//private void HandleEndGameEvent(EndGameEventPayload payload)
		//{

		//	GameMessage = payload.Message;
		//	GameStatus = payload.Status;

		//	UpdateGameBoard(payload.Tiles.ToList());

		//	A1IsEnabled = false;
		//	B1IsEnabled = false;
		//	C1IsEnabled = false;

		//	A2IsEnabled = false;
		//	B2IsEnabled = false;
		//	C2IsEnabled = false;

		//	A3IsEnabled = false;
		//	B3IsEnabled = false;
		//	C3IsEnabled = false;

		//}

		private void HandlePlayCommand(object sender)
		{

		//	eventAggregator.GetEvent<TraceEvent>().Publish(new TraceEventPayload());

		//	// Disable board
		//	PlayCommand.CanExecute(false);
		//	PlayCommand.RaiseCanExecuteChanged();

		//	// Send Move
		//	var address = sender.ToString();
		//	var payload = new TurnResponseEventPayload
		//	{
		//		Address = address,
		//	};
		//	eventAggregator.GetEvent<TurnResponseEvent>().Publish(payload);

		}

	}

}

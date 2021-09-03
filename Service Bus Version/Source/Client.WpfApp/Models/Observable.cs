namespace Gamer.Client.WpfApp.Models
{
	public class Observable<T> : ObservableObject
	{

		private T value;
		public T Value
		{
			get { return value; }
			set
			{
				if (this.value != null && this.value.Equals(value))
					return;
				this.value = value;
				RaisePropertyChanged();
			}

		}

	}
}
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Gamer.Client.WpfApp.Models
{

	/// <summary>
	/// Swiped, stripped, and minimized from NotificationObject in the Prism v4.1 library
	/// </summary>
	public abstract class ObservableObject : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void RaisePropertyChanged(params string[] propertyNames)
		{

			if (propertyNames == null)
				throw new ArgumentNullException(nameof(propertyNames));

			foreach (var name in propertyNames)
				// ReSharper disable once ExplicitCallerInfoArgument
				RaisePropertyChanged(name);

		}

		protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			var propertyName = ReflectionHelper.ExtractPropertyName(propertyExpression);
			if (propertyName != null)
				// ReSharper disable once ExplicitCallerInfoArgument
				RaisePropertyChanged(propertyName);
		}

	}

}

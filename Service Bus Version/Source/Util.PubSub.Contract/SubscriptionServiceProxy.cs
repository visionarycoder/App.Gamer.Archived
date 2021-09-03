using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using ServiceModelEx;

namespace Gamer.Util.PubSub.Interface
{

	public interface IGamerSubscriptionService : ISubscriptionService
	{
		
	}


	public class SubscriptionServiceProxy : DuplexClientBase<IGamerSubscriptionService>, IGamerSubscriptionService
	{
		public SubscriptionServiceProxy(object callbackInstance) : base(callbackInstance)
		{
		}

		public SubscriptionServiceProxy(object callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName)
		{
		}

		public SubscriptionServiceProxy(object callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public SubscriptionServiceProxy(object callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public SubscriptionServiceProxy(object callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
		{
		}

		public SubscriptionServiceProxy(object callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint)
		{
		}

		public SubscriptionServiceProxy(InstanceContext callbackInstance) : base(callbackInstance)
		{
		}

		public SubscriptionServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName) : base(callbackInstance, endpointConfigurationName)
		{
		}

		public SubscriptionServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public SubscriptionServiceProxy(InstanceContext callbackInstance, string endpointConfigurationName, EndpointAddress remoteAddress) : base(callbackInstance, endpointConfigurationName, remoteAddress)
		{
		}

		public SubscriptionServiceProxy(InstanceContext callbackInstance, Binding binding, EndpointAddress remoteAddress) : base(callbackInstance, binding, remoteAddress)
		{
		}

		public SubscriptionServiceProxy(InstanceContext callbackInstance, ServiceEndpoint endpoint) : base(callbackInstance, endpoint)
		{
		}

		public void Subscribe(string eventOperation)
		{
			Channel.Subscribe(eventOperation);
		}

		public void Unsubscribe(string eventOperation)
		{
			Channel.Subscribe(eventOperation);
		}

	}

}

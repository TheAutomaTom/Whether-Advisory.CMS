using System.Text;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace WA.CMS.Domain.Config
{
  public static class ElasticServiceCollectionExtension
  {

    public static void AddElasticSearch(this IServiceCollection services, IConfiguration config)
    {
      var url = config["ElasticConfiguration:Uri"];
      var defaultIndex = config["ElasticConfiguration:IndexUnconfigured"];
      var examplesIndex = config["ElasticConfiguration:IndexExamples"];
      var subscriptionsIndex = config["ElasticConfiguration:IndexSubscriptions"];
      var deliverablesIndex = config["ElasticConfiguration:IndexDeliverables"];

      var settings = new ConnectionSettings(new Uri(url))
          .PrettyJson() // Return human readable search results
          .DefaultIndex(defaultIndex)
          //.DefaultMappingFor<Subscription>(m => m.IndexName(subscriptionsIndex))
          //.DefaultMappingFor<Deliverable>(m => m.IndexName(deliverablesIndex))
          //.EnableHttpCompression()
          //.DisableDirectStreaming()
          .OnRequestCompleted(response => LogTransactions(response));

      //addDefaultMappings(settings);

      var client = new ElasticClient(settings);

      //client.Map<Subscription>(m => m.Index(subscriptionsIndex).AutoMap());
      //client.Map<Deliverable>(m => m.Index(deliverablesIndex).AutoMap());

      services.AddSingleton<IElasticClient>(client);

    }


    public static void LogTransactions(IApiCallDetails details)
    {
      // Log request
      if (details.RequestBodyInBytes != null)
      {
        Console.WriteLine(
        $"{details.HttpMethod} {details.Uri} \n" +
            $"{Encoding.UTF8.GetString(details.RequestBodyInBytes)}\n\r");
      }
      else
      {
        Console.WriteLine($"{details.HttpMethod} {details.Uri}\n\r");
      }
      //Log details
      if (details.ResponseBodyInBytes != null)
      {
        Console.WriteLine(
            $"{details.HttpMethod} {details.Uri} \n");
      }
      else
      {
        Console.WriteLine($"Status: {details.HttpMethod}\n");
      }

      Console.WriteLine($"{new string('-', 30)}\n\r");
    }

    //static void addDefaultMappings(ConnectionSettings settings)
    //{
    //    settings.DefaultMappingFor<SubscriptionScope>(s => 
    //        // Ignore irrelevant data in searches;
    //           //s.Ignore(p => p.Id)
    //           // .Ignore(p => p.SubscriberId)
    //        );
    //    settings.DefaultMappingFor<NotificationScope>(s =>
    //           // Ignore irrelevant data in searches;
    //           s.Ignore(p => p.Id)
    //            .Ignore(p => p.NotificationId)
    //        );
    //}

    //static void createIndex(IElasticClient client, string indexName ) =>
    //    client.Indices.CreateServiceFor(indexName, i => 
    //        i.Map<Subscription>(s=>s.AutoMap())
    //        //.Map<Example>(s=>s.AutoMap())

    //    );
  }
}



/*  Notes...
 
         public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticConfiguration = configuration.Get<ElasticsearchConfiguration>("elasticSearch");
            services.AddSingleton<IProductSearchService>(s =>
            {
                var client = ElasticClientFactory.CreateServiceFor(elasticConfiguration.PartIndex);
                return new ProductSearchService(client);
            });

            // make model index
            services.AddSingleton<IMakeModelSearchService>(s =>
            {
                var client = ElasticClientFactory.CreateServiceFor(elasticConfiguration.MakeModelIndex);
                return new MakeModelSearchService(client);
            });

            return services;
        }
 
 */

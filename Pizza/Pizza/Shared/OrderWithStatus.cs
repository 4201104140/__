//using System;
//using System.Collections.Generic;

//namespace BlazingPizza
//{
//    public class OrderWithStatus
//    {
//        public readonly static TimeSpan PreparationDuration = TimeSpan.FromSeconds(10);
//        public readonly static TimeSpan DeliveryDuration = TimeSpan.FromMinutes(1); // Unrealistic, but more interesting to watch

//        public Order Order { get; set; }

//        public string StatusText { get; set; }

//        public bool IsDelivered => StatusText == "Deliverd";

//        //public List<Mar>

//        public static OrderWithStatus FromOrder(Order order)
//        {
//            string statusText;

//            var dispatchTime = order.CreatedTime.Add(PreparationDuration);

//            if (DateTime.Now < dispatchTime)
//            {
//                statusText = "Preparing";
                
//            }
//            else if (DateTime.Now < dispatchTime + DeliveryDuration)
//            {
//                statusText = "Out for delivery";


//            }
//        }
//    }
//}

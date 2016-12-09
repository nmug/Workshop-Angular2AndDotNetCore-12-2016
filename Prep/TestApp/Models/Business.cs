using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace TestApp.Models
{
    [DataContract]
    public class Business
    {
        public class Option
        {
            public string formatted_original_price { get; set; }
            public string formatted_price { get; set; }
            public bool is_quantity_limited { get; set; }
            public int original_price { get; set; }
            public int price { get; set; }
            public string purchase_url { get; set; }
            public int remaining_count { get; set; }
            public string title { get; set; }
        }

        public class Deal
        {
            public string currency_code { get; set; }
            public string image_url { get; set; }
            public List<Option> options { get; set; }
            public string url { get; set; }
            public bool is_popular { get; set; }
            public int time_start { get; set; }
            public string title { get; set; }
        }

        public class Option2
        {
            public string formatted_price { get; set; }
            public int price { get; set; }
        }

        public class GiftCertificate
        {
            public string currency_code { get; set; }
            public string image_url { get; set; }
            public List<Option2> options { get; set; }
            public string url { get; set; }
            public string id { get; set; }
            public string unused_balances { get; set; }
        }

        public class Coordinate
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Location
        {
            public List<string> address { get; set; }
            public string city { get; set; }
            public Coordinate coordinate { get; set; }
            public string country_code { get; set; }
            public string cross_streets { get; set; }
            public List<string> display_address { get; set; }
            public double geo_accuracy { get; set; }
            public List<string> neighborhoods { get; set; }
            public string postal_code { get; set; }
            public string state_code { get; set; }
        }

        public class User
        {
            public string id { get; set; }
            public string image_url { get; set; }
            public string name { get; set; }
        }

        public class Review
        {
            public string excerpt { get; set; }
            public string id { get; set; }
            public int rating { get; set; }
            public string rating_image_large_url { get; set; }
            public string rating_image_small_url { get; set; }
            public string rating_image_url { get; set; }
            public int time_created { get; set; }
            public User user { get; set; }
        }

        //public class RootObject
        //{
        [DataMember]
        public List<List<string>> categories { get; set; }

        [DataMember]
        public List<Deal> deals { get; set; }
        [DataMember]
        public string display_phone { get; set; }
        [DataMember]
        public string eat24_url { get; set; }
        [DataMember]
        public List<GiftCertificate> gift_certificates { get; set; }
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string image_url { get; set; }
        [DataMember]
        public bool is_claimed { get; set; }
        [DataMember]
        public bool is_closed { get; set; }
        [DataMember]
        public Location location { get; set; }
        [DataMember]
        public int menu_date_updated { get; set; }
        [DataMember]
        public string menu_provider { get; set; }
        [DataMember]
        public string mobile_url { get; set; }

        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string phone { get; set; }

        [DataMember]
        public double rating { get; set; }
        [DataMember]
        public string rating_img_url { get; set; }
        [DataMember]
        public string rating_img_url_large { get; set; }
        [DataMember]
        public string rating_img_url_small { get; set; }
        [DataMember]
        public int review_count { get; set; }
        [DataMember]
        public List<Review> reviews { get; set; }
        [DataMember]
        public string snippet_image_url { get; set; }
        [DataMember]
        public string snippet_text { get; set; }
        [DataMember]
        public string url { get; set; }

    }
}

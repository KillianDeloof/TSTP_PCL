using TSTP_PCL.Models;
using TSTP_PCL.Filters;
using TSTP_PCL.MobileSDK.AzureMobileClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TSTP_PCL.Repositories
{
    public class APIRepository
    {
        /// <summary>
        /// sends the completed ticket to the api (use ticketObj.FormatTicket to complete the ticket)
        /// </summary>
        /// <param name="t">the ticket to be send</param>
        /// <returns></returns>
        public async Task SendTicket(Ticket t)
        {
            String res = await AzureMobileClient.DefaultClient.InvokeApiAsync<Ticket, string>("/api/OSTicket", t, System.Net.Http.HttpMethod.Post, null, System.Threading.CancellationToken.None);
        }


        /// <summary>
        /// Ophalen van de lijst van gebouwen, aangeleverd door de API.
        /// </summary>
        /// <returns>Task<List<Building>></returns>
        public async Task<List<Building>> GetBuildingList()
        {
            try {
                List<Building> result = new List<Building>();
                String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<string>("/api/Building", System.Net.Http.HttpMethod.Get, null, System.Threading.CancellationToken.None);
                List<Building> page = JsonConvert.DeserializeObject<List<Building>>(pagejson);
                result.AddRange(page);

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
}

        /// <summary>
        /// Ophalen van de lijst van campussen, aangeleverd door de API.
        /// </summary>
        /// <returns>Task<List<Campus>></returns>
        public static async Task<List<Campus>> GetCampusList()
        {
            try {
                List<Campus> result = new List<Campus>();
                String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<string>("/api/Campus", System.Net.Http.HttpMethod.Get, null, System.Threading.CancellationToken.None);
                List<Campus> page = JsonConvert.DeserializeObject<List<Campus>>(pagejson);
                result.AddRange(page);

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
}

        /// <summary>
        /// gets the wings from the api
        /// </summary>
        /// <returns>list of wing objects</returns>
        public async Task<List<Wing>> GetWingList()
        {
            try {
                List<Wing> result = new List<Wing>();
                String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<string>("/api/Wing", System.Net.Http.HttpMethod.Get, null, System.Threading.CancellationToken.None);
                List<Wing> page = JsonConvert.DeserializeObject<List<Wing>>(pagejson);
                result.AddRange(page);

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
}


        /// <summary>
        /// gets a list of floors from the api
        /// </summary>
        /// <returns>a list of floor objects</returns>
        public async Task<List<Floor>> GetFloorList()
        {
            try {
                List<Floor> result = new List<Floor>();
                FloorFilter ff = new FloorFilter();
                String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<FloorFilter, string>("/api/FloorSearch", ff, System.Net.Http.HttpMethod.Post, null, System.Threading.CancellationToken.None);
                List<Floor> page = JsonConvert.DeserializeObject<List<Floor>>(pagejson);
                result.AddRange(page);

                while (page.Count == ff.EffectivePageSize)
                {
                    ff.LastFloor = page[page.Count - 1];
                    pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<FloorFilter, string>("/api/FloorSearch", ff, System.Net.Http.HttpMethod.Post, null, System.Threading.CancellationToken.None);
                    page = JsonConvert.DeserializeObject<List<Floor>>(pagejson);
                    result.AddRange(page);
                }

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
}

        /// <summary>
        /// gets a list of rooms from api
        /// </summary>
        /// <param name="rf">room filter</param>
        /// <returns>returns a list of room objects</returns>
        public async Task<List<Room>> GetRoomList()
        {
            try
            {
                List<Room> result = new List<Room>();
                RoomFilter rf = new RoomFilter();
                String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<RoomFilter, string>("/api/RoomSearch", rf, System.Net.Http.HttpMethod.Post, null, System.Threading.CancellationToken.None);
                List<Room> page = JsonConvert.DeserializeObject<List<Room>>(pagejson);
                result.AddRange(page);

                while (page.Count == rf.EffectivePageSize)
                {
                    rf.LastRoom = page[page.Count - 1];
                    pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<RoomFilter, string>("/api/RoomSearch", rf, System.Net.Http.HttpMethod.Post, null, System.Threading.CancellationToken.None);
                    page = JsonConvert.DeserializeObject<List<Room>>(pagejson);
                    result.AddRange(page);
                }

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// gets the forums from the api
        /// </summary>
        /// <returns>returns a list of forum objects</returns>
        public async Task<List<Forum>> GetForumList()
        {
            try {
                List<Forum> result = new List<Forum>();
                String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<string>("/api/forum", System.Net.Http.HttpMethod.Get, null, System.Threading.CancellationToken.None);
                List<Forum> page = JsonConvert.DeserializeObject<List<Forum>>(pagejson);
                result.AddRange(page);

                return result;
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.Message);
                return null;
            }
}

        /// <summary>
        /// gets a hardcoded list of the categorys
        /// </summary>
        /// <returns>a list of category objects, including their sup categorys</returns>
        public List<MainCategory> GetHardCodedCategoryList()
        {
            List<MainCategory> list = new List<MainCategory>();

            MainCategory campus = new MainCategory();
            campus.CategoryUDesc = "Campus";
            campus.Picture = "ic_location_city_black_24dp.png";
            list.Add(campus);

            MainCategory faciliteiten = new MainCategory();
            faciliteiten.CategoryUDesc = "Facilities";
            faciliteiten.Picture = "ic_directions_bus_black_24dp.png";
            list.Add(faciliteiten);

            MainCategory lesmateriaal = new MainCategory();
            lesmateriaal.CategoryUDesc = "Study Materials";
            lesmateriaal.Picture = "ic_book_black_24dp.png";
            list.Add(lesmateriaal);

            MainCategory netwerk = new MainCategory();
            netwerk.CategoryUDesc = "Network";
            netwerk.Picture = "ic_settings_input_hdmi_black_24dp.png";
            list.Add(netwerk);

            MainCategory softwareHardware = new MainCategory();
            softwareHardware.CategoryUDesc = "Software & hardware";
            softwareHardware.Picture = "ic_laptop_black_24dp.png";
            list.Add(softwareHardware);

            MainCategory organisatie = new MainCategory();
            organisatie.CategoryUDesc = "Organization";
            organisatie.Picture = "ic_people_black_24dp.png";
            list.Add(organisatie);

            MainCategory other = new MainCategory();
            other.CategoryUDesc = "Other";
            other.Picture = "ic_priority_high_black_24dp.png";
            list.Add(other);

            foreach (MainCategory cat in list)
            {
                cat.SubCategoryList = GetHardcodedSubCat(cat);
            }

            return list;
        }

        /// <summary>
        /// Get the sub categories of a main category
        /// </summary>
        /// <param name="cat">main category</param>
        /// <returns>A list of sub categories</returns>

        private static List<Category> GetHardcodedSubCat(MainCategory cat)
        {
            List<Category> list = new List<Category>();

            Category bookSales = new Category();
            bookSales.SubCategoryUDesc = "BookSales";

            Category financial = new Category();
            financial.SubCategoryUDesc = "Financial";

            switch (cat.CategoryUDesc)
            {
                case "Campus":
                    Category cateringAndVending = new Category();
                    cateringAndVending.SubCategoryUDesc = "Catering And Vending Machines";
                    cateringAndVending.IsLocationRequired = true;
                    list.Add(cateringAndVending);

                    Category furniture = new Category();
                    furniture.SubCategoryUDesc = "Furniture";
                    furniture.IsLocationRequired = true;
                    list.Add(furniture);

                    Category wasteManegment = new Category();
                    wasteManegment.SubCategoryUDesc = "Waste Manegment";
                    wasteManegment.IsLocationRequired = true;
                    list.Add(wasteManegment);

                    Category sanitary = new Category();
                    sanitary.SubCategoryUDesc = "Sanitary";
                    sanitary.IsLocationRequired = true;
                    list.Add(sanitary);

                    Category classRooms = new Category();
                    classRooms.SubCategoryUDesc = "ClassRooms and Maintenance";
                    classRooms.IsLocationRequired = true;
                    list.Add(classRooms);

                    break;

                case "Facilities":
                    Category mobility = new Category();
                    mobility.SubCategoryUDesc = "Mobility";
                    list.Add(mobility);

                    list.Add(bookSales);

                    Category sportOffers = new Category();
                    sportOffers.SubCategoryUDesc = "Sport Offers";
                    list.Add(sportOffers);

                    Category printing = new Category();
                    printing.SubCategoryUDesc = "Printing";
                    list.Add(printing);

                    Category studdySupport = new Category();
                    studdySupport.SubCategoryUDesc = "Study Support";
                    list.Add(studdySupport);

                    list.Add(financial);

                    break;

                case "Study Materials":
                    Category studyMaterials = new Category();
                    studyMaterials.SubCategoryUDesc = "Study Materials";
                    list.Add(studyMaterials);

                    list.Add(bookSales);

                    Category studyPlatform = new Category();
                    studyPlatform.SubCategoryUDesc = "Study Platform";
                    list.Add(studyPlatform);

                    list.Add(financial);

                    break;

                case "Network":
                    Category wifi = new Category();
                    wifi.SubCategoryUDesc = "WiFi";
                    wifi.IsLocationRequired = true;
                    list.Add(wifi);

                    Category networkAcces = new Category();
                    networkAcces.SubCategoryUDesc = "Network Access";
                    list.Add(networkAcces);

                    Category netWorkMaintenance = new Category();
                    netWorkMaintenance.SubCategoryUDesc = "Network Maintenance";
                    netWorkMaintenance.IsLocationRequired = true;
                    list.Add(netWorkMaintenance);

                    Category generalNetwork = new Category();
                    generalNetwork.SubCategoryUDesc = "General Network";
                    list.Add(generalNetwork);

                    break;

                case "Software & hardware":
                    Category softWare = new Category();
                    softWare.SubCategoryUDesc = "SoftWare";
                    list.Add(softWare);

                    Category sharepointDocenten = new Category();
                    sharepointDocenten.SubCategoryUDesc = "Sharepoint Docenten";
                    sharepointDocenten.IsStaffRequired = true;
                    list.Add(sharepointDocenten);

                    Category helpdesk = new Category();
                    helpdesk.SubCategoryUDesc = "HelpDesk AND Remote Helpdesk";
                    list.Add(helpdesk);

                    Category macSupport = new Category();
                    macSupport.SubCategoryUDesc = "Mac Support";
                    list.Add(macSupport);

                    Category signPost = new Category();
                    signPost.SubCategoryUDesc = "SignPost";
                    list.Add(signPost);

                    break;

                case "Organization":
                    Category generalOrganization = new Category();
                    generalOrganization.SubCategoryUDesc = "General organization";
                    list.Add(generalOrganization);

                    Category events = new Category();
                    events.SubCategoryUDesc = "Events";
                    list.Add(events);

                    Category onderwijsOrganizatie = new Category();
                    onderwijsOrganizatie.SubCategoryUDesc = "Educational organization";
                    list.Add(onderwijsOrganizatie);

                    break;

                case "Other":
                    Category energyManegment = new Category();
                    energyManegment.SubCategoryUDesc = "Energy Manegment";
                    list.Add(energyManegment);

                    Category other = new Category();
                    other.SubCategoryUDesc = "Other";
                    list.Add(other);

                    break;

                default:
                    break;
            }


            return list;
        }


        //public async Task<List<MainCategory>> GetCategoryList()
        //{
        //    try
        //    {
        //        List<MainCategory> result = new List<MainCategory>();
        //        String pagejson = await AzureMobileClient.DefaultClient.InvokeApiAsync<string>("/api/category", System.Net.Http.HttpMethod.Get, null, System.Threading.CancellationToken.None);
        //        List<MainCategory> page = JsonConvert.DeserializeObject<List<MainCategory>>(pagejson);
        //        result.AddRange(page);

        //        //List<String> categoryList = new List<string>();
        //        //result.ForEach(c => Console.WriteLine(c.CategoryUCode));

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //        return null;
        //    }
        //}

    }
}

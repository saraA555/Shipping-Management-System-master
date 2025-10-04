﻿namespace ITI.Shipping.Core.Application.Abstraction.Branch.Models
{
    public class BranchDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public  string? Location { get; set; }
        public DateTime BranchDate { get; set; } 
        public bool IsDeleted { get; set; }
        //----------- Region ---------------------------------
        public int? RegionId { get; set; }
        public string? RegionName { get; set; }
        //-----------  CitySetting ---------------------------------
        public int? CitySettingId { get; set; }
        public string? CityName { get; set; }
        //------------- List From User ------------------------------
        public List<string> UsersName { get; set; } = [];
    }
}

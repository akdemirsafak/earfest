﻿using earfest.API.Models.Contents;

namespace earfest.API.Models.Moods;

public class MoodDetailsResponse : BaseResponse
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public virtual IList<ContentResponse> Contents { get; set; }

}

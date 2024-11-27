using Shared.Model.Records;

namespace Shared.Model.Messages;

public class GeneratingWishlist {
    public int ExperimentId { get; set; } 
    public int HackathonCount {get; set;}
    public Wishlist Wishlist { get; set; }
        
};
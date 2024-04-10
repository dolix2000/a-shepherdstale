using Assets.Scripts.ItemFactory;
using Assets.Scripts.Items;

/// <summary>
/// This class allows us to create a specific boost
/// </summary>
public class BoostFactory 
{
    /// <summary>
    /// Method for creating boosts, based on their type
    /// </summary>
    /// <param name="type">of the boost, that gets created</param>
    /// <returns>Created boost</returns>
    public Boost CreateBoost(BoostType type)
    {
        Boost boost = null;

        switch (type)
        {
            case BoostType.SPEEDBOOST:
                boost = new SpeedBoost();
                break;
            case BoostType.TIMEBOOST:
                boost = new TimeBoost();
                break;
            case BoostType.JUMPBOOST:
                boost = new JumpBoost();
                break;
        }
        return boost;
    }
}

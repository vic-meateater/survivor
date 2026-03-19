namespace Code.Gameplay.Features.TargetCollection
{
  public static class TargetCollectionEntityExtensions
  {
    public static GameEntity RemoveTargetCollectionComponents(this GameEntity entity)
    {
      if (entity.hasTargetBuffer)
        entity.RemoveTargetBuffer();

      if (entity.hasCollectsTargetInterval)
        entity.RemoveCollectsTargetInterval();

      if (entity.hasCollectsTargetTimer)
        entity.RemoveCollectsTargetTimer();

      entity.isReadyToCollectTargets = false;
      
      return entity;
    }
  }
}
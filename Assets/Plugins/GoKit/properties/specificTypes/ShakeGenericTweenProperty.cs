using UnityEngine;
using System.Collections;


public class ShakeGenericTweenProperty : AbstractTweenProperty
{
	private AbstractTweenProperty _tweenProperty;
	
	private int _frameCount;
	private int _frameMod;
	
	
	/// <summary>
	/// you can shake any AbstractTweenProperty. frameMod allows you to specify 
	/// what frame count the shakes should occur on. for example, a frameMod of 3 would mean that only when
	/// frameCount % 3 == 0 will the shake occur
	/// </summary>
	public ShakeGenericTweenProperty( AbstractTweenProperty tweenPorperty, int frameMod = 1 ) : base( true )
	{
		_tweenProperty = tweenPorperty;
		_frameMod = frameMod;
	}
	
	
	#region Object overrides
	
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
	
	
	public override bool Equals( object obj )
	{
		// start with a base check and then compare our material names
		return _tweenProperty.Equals(((ShakeGenericTweenProperty)obj)._tweenProperty);
	}
	
	#endregion
	
	public override void init( GoTween owner )
	{
		base.init(owner);
		//Init owner of _tweenProperty and force ease type to linear
		_tweenProperty.setEaseType(GoEaseType.Linear);
		_tweenProperty.init(owner);
	}

	public override bool validateTarget( object target )
	{
		return _tweenProperty.validateTarget(target);
	}
	
	
	public override void prepareForUse()
	{
		_tweenProperty.prepareForUse();
		_frameCount = 0;
	}


	public override void tick( float totalElapsedTime )
	{
		// should we skip any frames?
		if( _frameMod > 1 && ++_frameCount % _frameMod != 0 )
			return;
		
		// _ownerTween is supposed to have a GoEaseLinear easeType
		float timeLimit=_ownerTween.duration-_easeFunction(totalElapsedTime, 0, _ownerTween.duration, _ownerTween.duration);
		var randomTime=Random.Range(-timeLimit,timeLimit);
		
		_tweenProperty.tick(randomTime);
	}
}

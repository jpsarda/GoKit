using UnityEngine;
using System.Collections;


public abstract class AbstractDoubleTweenProperty : AbstractTweenProperty
{
	protected AbstractTweenProperty _tweenProperty;

	/// <summary>
	/// reference another tween property
	/// </summary>
	public AbstractDoubleTweenProperty( AbstractTweenProperty tweenPorperty ) : base( true )
	{
		_tweenProperty = tweenPorperty;
	}
	
	
	#region Object overrides
	
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}
	
	
	public override bool Equals( object obj )
	{
		// start with a base check and then compare our material names
		return _tweenProperty.Equals(((AbstractDoubleTweenProperty)obj)._tweenProperty);
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
	}
}

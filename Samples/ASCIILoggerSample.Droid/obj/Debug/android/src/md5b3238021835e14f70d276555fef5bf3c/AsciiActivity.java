package md5b3238021835e14f70d276555fef5bf3c;


public class AsciiActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("ASCIILoggerSample.Droid.AsciiActivity, ASCIILoggerSample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AsciiActivity.class, __md_methods);
	}


	public AsciiActivity ()
	{
		super ();
		if (getClass () == AsciiActivity.class)
			mono.android.TypeManager.Activate ("ASCIILoggerSample.Droid.AsciiActivity, ASCIILoggerSample.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}

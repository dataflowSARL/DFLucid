package md57c883242be2a3677065c75adf70e4be4;


public class ViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("lucid.ViewHolder, lucid", ViewHolder.class, __md_methods);
	}


	public ViewHolder ()
	{
		super ();
		if (getClass () == ViewHolder.class)
			mono.android.TypeManager.Activate ("lucid.ViewHolder, lucid", "", this, new java.lang.Object[] {  });
	}

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

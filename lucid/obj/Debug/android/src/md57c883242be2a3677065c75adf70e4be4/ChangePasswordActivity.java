package md57c883242be2a3677065c75adf70e4be4;


public class ChangePasswordActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"n_onStart:()V:GetOnStartHandler\n" +
			"n_onResume:()V:GetOnResumeHandler\n" +
			"n_onUserInteraction:()V:GetOnUserInteractionHandler\n" +
			"";
		mono.android.Runtime.register ("lucid.ChangePasswordActivity, lucid", ChangePasswordActivity.class, __md_methods);
	}


	public ChangePasswordActivity ()
	{
		super ();
		if (getClass () == ChangePasswordActivity.class)
			mono.android.TypeManager.Activate ("lucid.ChangePasswordActivity, lucid", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);


	public void onStart ()
	{
		n_onStart ();
	}

	private native void n_onStart ();


	public void onResume ()
	{
		n_onResume ();
	}

	private native void n_onResume ();


	public void onUserInteraction ()
	{
		n_onUserInteraction ();
	}

	private native void n_onUserInteraction ();

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

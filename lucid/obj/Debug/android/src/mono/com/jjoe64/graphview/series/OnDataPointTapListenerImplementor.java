package mono.com.jjoe64.graphview.series;


public class OnDataPointTapListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.jjoe64.graphview.series.OnDataPointTapListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTap:(Lcom/jjoe64/graphview/series/Series;Lcom/jjoe64/graphview/series/DataPointInterface;)V:GetOnTap_Lcom_jjoe64_graphview_series_Series_Lcom_jjoe64_graphview_series_DataPointInterface_Handler:AndroidGraphView.Series.IOnDataPointTapListenerInvoker, GraphViewBinding\n" +
			"";
		mono.android.Runtime.register ("AndroidGraphView.Series.IOnDataPointTapListenerImplementor, GraphViewBinding", OnDataPointTapListenerImplementor.class, __md_methods);
	}


	public OnDataPointTapListenerImplementor ()
	{
		super ();
		if (getClass () == OnDataPointTapListenerImplementor.class)
			mono.android.TypeManager.Activate ("AndroidGraphView.Series.IOnDataPointTapListenerImplementor, GraphViewBinding", "", this, new java.lang.Object[] {  });
	}


	public void onTap (com.jjoe64.graphview.series.Series p0, com.jjoe64.graphview.series.DataPointInterface p1)
	{
		n_onTap (p0, p1);
	}

	private native void n_onTap (com.jjoe64.graphview.series.Series p0, com.jjoe64.graphview.series.DataPointInterface p1);

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

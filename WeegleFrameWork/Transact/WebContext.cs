using System;
using System.Web ;
using System.Data ;
using System.Web.UI;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.SessionState;

namespace Weegle.FrameWork.WeegleFrameWork
{
	/// <summary>
	/// Context ��ժҪ˵����
	/// </summary>
	public class WebContext : IContext
	{
		protected Control _parent;
		protected IEnumerable[] _collections;
		protected HttpRequest _request;
		protected HttpSessionState _session;
		protected string _prex = null;
		protected IEnumerable _current = null;

		/// <summary>
		/// �����ڶ���ؼ���ȡֵʱ�������ֵ�prex
		/// </summary>
		public string Prex
		{
			set{
				if(value != null && value != "")
				{
					_prex = value + "_";
				}
				else
				{
					_prex = value;
				}
				}
			get{return _prex;}
		}

		public IEnumerable Current
		{
			set { _current = value ;}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <param name="session"></param>
		/// <param name="collection">������������֯�ļ���,������DataView,Hashtable </param>
		public WebContext( HttpRequest request , HttpSessionState session , Control control , params IEnumerable[] collections)
		{
			_parent = control ;
			_session = session ;
			_request = request ;
			_collections = collections ;
		}

		/// <summary>
		/// keyΪ {xxxx},{[xxxx]},xxxx
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public object GetValue( string key )
		{
			object getValue = null;
			key = key.ToLower();
			if( key.IndexOf("{[") != -1 || key.IndexOf("{") == -1)
			{
				if(key.IndexOf("{[") != -1)
				{
					key = key.Substring(2, key.Length - 4);
				}
				if(_current != null)
				{
					getValue = GetValueFromCollection( _current , key ); 
					if(getValue != null)
					{
						return getValue;
					}
				}

				if( _request != null )
				{
					string tep = _prex + key;
					if((getValue = _request.Form[tep]) != null)
					{
						getValue = getValue.ToString().Replace("'","''"); 
						return getValue ;
					}
					else if((getValue = _request.QueryString[tep]) != null)
					{
						getValue = getValue.ToString().Replace("'","''"); 
						return getValue;
					}

				}
				if ( _parent != null )
				{
					for ( int i = 0 ; i < _parent.Controls.Count ; i ++ )
					{
						getValue = GetValueFromControl( _parent.Controls[i] , key ) ;
						if ( getValue != null )
						{
							return getValue;
						}
					}
				}
				if ( _collections != null )
				{
					for ( int i = 0 ; i < _collections.Length ; i ++ )
					{
						if (_collections[i] == null) 
						{
							continue;
						}
						getValue = GetValueFromCollection( _collections[i] , key ); 
						if ( getValue != null )
						{
							return getValue;
						}
					}
				}
			}
			else if (_session != null && key.IndexOf("{") != -1)
			{
				key = key.Substring(1, key.Length - 2);
				return _session[key];
			}
			return getValue ;
		}

		#region ˽�з���
		private object GetValueFromControl( Control control , string key)
		{
			return null ;
		}

		private object GetValueFromCollection( IEnumerable collection , string key )
		{
			if ( collection.GetType().Name.ToLower().Equals( "hashtable" ))
			{
				Hashtable hash = ( Hashtable ) collection ;
				if ( hash[ key.ToLower() ] != null )
				{
					return hash[ key ] ;
				}
			}
			else if ( collection.GetType().Name.ToLower().Equals( "dataview" ) )
			{
				DataView dv = ( DataView ) collection ;
				for (int i = 0 ; i < dv.Count ; i ++ )
				{
					if(dv.Table.Columns.Contains(key))
					{
						if ( dv[i][key] != null )
						{
							return dv[i][key] ;
						}
					}
				}
			}
			return null;
		}	
		#endregion
	}

}

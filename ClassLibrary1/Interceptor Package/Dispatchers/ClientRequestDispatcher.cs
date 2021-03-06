﻿
using Helpers.Interceptor_Package.Interceptors;
using System;
using System.Collections.Generic;
using System.Diagnostics;



namespace Helpers.Interceptor_Package.Dispatchers
{
   public  class ClientRequestDispatcher : Dispatcher

    {
        protected readonly object _syncRoot = new Object();
        private static ClientRequestDispatcher instanciatedObject;
        private List<Interceptor> interceptorList;



        public ClientRequestDispatcher() { interceptorList = new List<Interceptor>(); }


        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void dispatchClientRequestInterceptorReadDatabaseRequest(DataBaseReadRequest context)
        {
            List<Interceptor> interceptors;
            lock (_syncRoot)
            { // Clone vector.
                interceptors = (List<Interceptor>)interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                ClientRequestInterceptor ic =
                (ClientRequestInterceptor)interceptors[i];
                // Dispatch callback hook method.
                ic.onDatabaseReadRequest(context);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void dispatchClientRequestInterceptorWriteDatabaseRequest(DatabaseWriteRequest context)
        {
            List<Interceptor> interceptors;
            lock (_syncRoot)
            { // Clone vector.
                interceptors = (List<Interceptor>)interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                ClientRequestInterceptor ic =
                (ClientRequestInterceptor)interceptors[i];
                // Dispatch callback hook method.
                ic.onDatabaseWriteRequest(context);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void dispatchClientRequestInterceptorTransactionAttempt(TransactionInfo context)
        {
            List<Interceptor> interceptors;
            lock (_syncRoot)
            { // Clone vector.
                interceptors = (List<Interceptor>)interceptorList; ;
            }

            for (int i = 0; i < interceptors.Count; ++i)
            {
                ClientRequestInterceptor ic =
                (ClientRequestInterceptor)interceptors[i];
                // Dispatch callback hook method.
                ic.onTransactionAttempted(context);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public static ClientRequestDispatcher theInstance()
        {
            if (instanciatedObject == null)
            {
                Debug.WriteLine("Creating new ClientRequestDispatcher");
                //creating new Instanciated ClientRequestDispatcher
                instanciatedObject = new ClientRequestDispatcher();
                return instanciatedObject;
            }
            else
            {
                
                return instanciatedObject;//returning previously made instanciated ClientRequestDispatcher
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void registerClientInterceptor(Interceptor i)
        {   //adding clientInterceptor
            this.interceptorList.Add(i);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        public void unregisterClientInterceptor(Interceptor i)
        {   //removing clientInterceptor
            this.interceptorList.Remove(i);
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    }
}

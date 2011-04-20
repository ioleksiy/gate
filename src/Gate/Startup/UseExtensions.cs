﻿using System;
using System.Collections.Generic;

namespace Gate.Startup
{
    using AppDelegate = Action< // app
        IDictionary<string, object>, // env
        Action< // result
            string, // status
            IDictionary<string, string>, // headers
            Func< // body
                Func< // next
                    ArraySegment<byte>, // data
                    Action, // continuation
                    bool>, // async                    
                Action<Exception>, // error
                Action, // complete
                Action>>, // cancel
        Action<Exception>>; // error

    public static class UseExtensions
    {
        /* 
         * extension methods take an AppDelegate factory func and it's associated parameters
         */

        public static AppBuilder Use<T1>(this AppBuilder builder, Func<AppDelegate, T1, AppDelegate> factory, T1 arg1)
        {
            return builder.Use(app => factory(app, arg1));
        }

        public static AppBuilder Use<T1, T2>(this AppBuilder builder, Func<AppDelegate, T1, T2, AppDelegate> factory, T1 arg1, T2 arg2)
        {
            return builder.Use(app => factory(app, arg1, arg2));
        }

        public static AppBuilder Use<T1, T2, T3>(this AppBuilder builder, Func<AppDelegate, T1, T2, T3, AppDelegate> factory, T1 arg1, T2 arg2, T3 arg3)
        {
            return builder.Use(app => factory(app, arg1, arg2, arg3));
        }

        public static AppBuilder Use<T1, T2, T3, T4>(this AppBuilder builder, Func<AppDelegate, T1, T2, T3, T4, AppDelegate> factory, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            return builder.Use(app => factory(app, arg1, arg2, arg3, arg4));
        }

        
        /* 
         * extension methods take a type implemeting IMiddleware and it's associated parameters
         */
        
        public static AppBuilder Use<TMiddleware>(this AppBuilder builder) where TMiddleware : IMiddleware, new()
        {
            return builder.Use(new TMiddleware().Create);
        }

        public static AppBuilder Use<TMiddleware, T1>(this AppBuilder builder, T1 arg1) where TMiddleware : IMiddleware<T1>, new()
        {
            return builder.Use(app => new TMiddleware().Create(app,arg1));
        }

        public static AppBuilder Use<TMiddleware, T1, T2>(this AppBuilder builder, T1 arg1, T2 arg2) where TMiddleware : IMiddleware<T1, T2>, new()
        {
            return builder.Use(app => new TMiddleware().Create(app,arg1, arg2));
        }

        public static AppBuilder Use<TMiddleware, T1, T2, T3>(this AppBuilder builder, T1 arg1, T2 arg2, T3 arg3) where TMiddleware : IMiddleware<T1, T2, T3>, new()
        {
            return builder.Use(app => new TMiddleware().Create(app,arg1, arg2, arg3));
        }

        public static AppBuilder Use<TMiddleware, T1, T2, T3, T4>(this AppBuilder builder, T1 arg1, T2 arg2, T3 arg3, T4 arg4) where TMiddleware : IMiddleware<T1, T2, T3, T4>, new()
        {
            return builder.Use(app => new TMiddleware().Create(app,arg1, arg2, arg3, arg4));
        }
    }
}
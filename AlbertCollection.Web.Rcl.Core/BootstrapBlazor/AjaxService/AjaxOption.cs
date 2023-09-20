#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace AlbertCollection.Web.Rcl.Core
{
    /// <summary>
    /// Ajax配置类
    /// </summary>
    public class AjaxOption
    {
        /// <summary>
        /// 获取/设置 要上传的参数类
        /// </summary>
        [NotNull]
        public object Data { get; set; }

        /// <summary>
        /// 获取/设置 传输方式，默认为POST
        /// </summary>
        public string Method { get; set; } = "POST";

        /// <summary>
        /// 获取/设置 请求的URL
        /// </summary>
        [NotNull]
        public string Url { get; set; }
    }
}
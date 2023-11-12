#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

global using Furion;
global using Furion.DependencyInjection;
global using Furion.EventBus;
global using Furion.FriendlyException;

global using Mapster;

global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.SignalR;
global using Microsoft.CodeAnalysis;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;

global using NewLife.Serialization;

global using SqlSugar;

global using System;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.Text;
global using System.Threading.Tasks;

global using AlbertCollection.Core;
global using AlbertCollection.Core.Extension;
global using AlbertCollection.Core.Utils;

global using Yitter.IdGenerator;

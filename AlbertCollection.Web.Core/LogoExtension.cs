#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using Furion.Templates;

using System.Drawing;

namespace AlbertCollection.Web.Core;


/// <summary>
/// logo显示
/// </summary>
public static class LogoExtension
{
    /// <summary>
    /// 添加Logo显示
    /// </summary>
    /// <param name="services"></param>
    public static void AddLogoDisplay(this IServiceCollection services)
    {
        Colorful.Console.WriteAsciiAlternating("AlbertCollection", new Colorful.FrequencyBasedColorAlternator(3, Color.Yellow, Color.GreenYellow));
        var template = TP.Wrapper("AlbertCollection边缘网关",
         "欢迎使用 AlbertZhao 的智能采集应用，该应用主要针对智能工厂，功能包括但不限于设备采集、设备监控等，理念为展望国家全方位进入工业 4.0 时代，开启智能制造的时代",
         "##作者## AlbertZhao",
         "##当前版本## " + Assembly.GetExecutingAssembly().GetName().Version,
         "##文档地址## " + @"https://www.yuque.com/albertzhao",
         "##作者信息## AlbertZhao 微信：zhy_cxx") + Environment.NewLine;
        Colorful.Console.WriteAlternating(template, new Colorful.FrequencyBasedColorAlternator(3, Color.Yellow, Color.GreenYellow));

    }
}
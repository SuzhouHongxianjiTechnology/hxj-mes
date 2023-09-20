#region copyright
//------------------------------------------------------------------------------
//  此代码版权声明为全文件覆盖，如有原作者特别声明，会在下方手动补充
//  此代码版权（除特别声明外的代码）归作者本人AlbertZhao所有
//  源代码使用协议遵循本仓库的开源协议及附加协议
//  Gitee源代码仓库：https://gitee.com/AlbertZhao/AlbertCollection



//------------------------------------------------------------------------------
#endregion

using BlazorComponent.I18n;

using NewLife;

namespace AlbertCollection.Web.Rcl.Core
{
    public class CultureComponentBase : BaseComponentBase
    {
        [CascadingParameter]
        public CultureInfo Culture { get; set; }

        [Inject]
        public I18n LanguageService { get; set; }

        public string ScopeT(string scope, string key, params object[] args)
        {
            return string.Format(LanguageService.T(scope, key, true), args);
        }

        public string T(string key, params object[] args)
        {
            if (key.IsNullOrEmpty())
            {
                return "";
            }
            return string.Format(LanguageService.T(key, false, key), args);
        }
    }
}
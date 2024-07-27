using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebMVC2.ViewModels;

namespace WebMVC2.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // 设置共享数据
            if (context.Controller is Controller controller && controller.ViewData.Model is BaseViewModel baseViewModel)
            {
                baseViewModel.SharedData1 = "Some shared data";
                baseViewModel.SharedData2 = "Other shared data";
                // 设置其他共享数据...
            }
        }
    }

    /*
    OnActionExecuting：在控制器的動作方法執行之前被呼叫，可以用於執行一些預處理邏輯。
    OnActionExecuted：在控制器的動作方法執行之後被呼叫，可以用於執行一些後處理邏輯。
    OnResultExecuting：在結果執行之前被呼叫，可以用於執行一些預處理邏輯。
    OnResultExecuted：在結果執行之後被呼叫，可以用於執行一些後處理邏輯。
    OnActionAuthorization：在動作方法的授權檢查之前被呼叫，可以用於自定義授權邏輯。
    OnActionAuthorizationFailed：在動作方法的授權檢查失敗時被呼叫，可以用於處理授權失敗的情況。
    OnException：在控制器的動作方法發生異常時被呼叫，可以用於處理異常情況。
    OnModelUpdated：在模型更新事件發生時被呼叫，可以用於處理模型更新的邏輯。
    OnInvalidModelState：在模型狀態無效時被呼叫，可以用於處理無效模型狀態的情況。
    OnViewExecuted：在檢視執行之後被呼叫，可以用於處理檢視執行後的邏輯。
     */
}

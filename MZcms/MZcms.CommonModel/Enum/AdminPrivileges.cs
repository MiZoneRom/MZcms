using MZcms.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MZcms.CommonModel
{
    public enum AdminPrivilege
    {
        //所有权限

        /*商品*/
        [Privilege("商品", "商品管理", 2001, "product/management", "product")]
        ProductManage = 2001,
        [Privilege("商品", "分类管理", 2002, "category/management", "category")]
        CategoryManage = 2002,
        [Privilege("商品", "品牌管理", 2003, "brand/Management", "brand")]

        BrandManage = 2003,
        [Privilege("商品", "类型管理", 2004, "ProductType/management", "ProductType")]
        ProductTypeManage = 2004,
        [Privilege("商品", "咨询管理", 2006, "ProductConsultation/management", "productconsultation")]
        ConsultationManage = 2006,
        [Privilege("商品", "评论管理", 2007, "ProductComment/management", "ProductComment")]
        CommentManage = 2007,
        ///*交易*/

        [Privilege("交易", "订单管理", 3001, "Order/management", "order")]
        OrderManage = 3001,
        [Privilege("交易", "退款处理", 3002, "OrderRefund/management?showtype=2", "orderrefund")]
        ReturnRefundManage = 3002,
        [Privilege("交易", "退货处理", 3009, "OrderRefund/management?showtype=3", "ReturnGoodsManage")]
        ReturnGoodsManage = 3009,
        [Privilege("交易", "交易评价", 3003, "OrderComment/management", "ordercomment")]
        OrderComment = 3003,
        [Privilege("交易", "交易投诉", 3004, "OrderComplaint/management", "ordercomplaint")]
        OrderComplaint = 3004,
        [Privilege("交易", "支付方式", 3005, "Payment/management", "payment")]
        PaymentManage = 3005,
        [Privilege("交易", "快递单模板", 3006, "ExpressTemplate/management", "ExpressTemplate")]
        ExpressTemplate = 3006,
        [Privilege("交易", "交易设置", 3007, "AdvancePayment/edit", "AdvancePayment")]
        PaymentManageSet = 3007,
        [Privilege("交易", "发票管理", 3008, "Order/InvoiceContext", "InvoiceContext")]
        InvoiceContextManage = 3008,

        ///*会员*/
        [Privilege("会员", "会员管理", 4001, "member/management", "member")]
        MemberManage = 4001,
        [Privilege("会员", "会员分组", 4002, "member/MemberGroup", "member")]
        MemberGroupManage = 4002,
        [Privilege("会员", "标签管理", 4009, "Label/management", "Label")]
        LabelManage = 4009,
        [Privilege("会员", "会员营销", 4010, "MessageGroup/WXGroupMessage", "MessageGroup")]
        MarketingManage = 4010,
        [Privilege("会员", "会员积分", 4003, "MemberIntegral/search", "MemberIntegral")]
        MemberIntegral = 4003,
        [Privilege("会员", "积分规则", 4004, "IntegralRule/management", "IntegralRule")]
        IntegralRule = 4004,
        [Privilege("会员", "会员等级", 4005, "MemberGrade/management", "MemberGrade")]
        MemberGrade = 4005,
        [Privilege("会员", "信任登录", 4006, "OAuth/Management", "OAuth")]
        OAuth = 4006,
        [Privilege("会员", "会员推广", 4007, "MemberInvite/Setting", "MemberInvite")]
        MemberInvite = 4007,
        [Privilege("会员", "预付款管理", 4008, "Capital/Index", "Capital")]
        Capital = 4008,

        ///*统计*/
        [Privilege("统计", "会员统计", 6002, "Statistics/Member", "statistics", "member")]
        MemberStatistics = 6002,
        [Privilege("统计", "店铺统计", 6003, "Statistics/NewShop", "statistics", "newshop")]
        ShopStatistics = 6003,
        [Privilege("统计", "商品统计", 6004, "Statistics/ProductSaleStatistic", "statistics")]
        ProductSaleStatistic = 6004,
        [Privilege("统计", "交易统计", 6005, "Statistics/TradeStatistic", "statistics")]
        TradeStatistic = 6005,

        ///*网站*/
        [Privilege("网站", "首页模板", 7001, "PageSettings/home", "PageSettings","home")]
        [Privilege("网站", "页面设置", 7001, "PageSettings", "SlideAd")]
        PageSetting = 7001,
        [Privilege("网站", "文章管理", 7002, "Article/management", "article")]
        ArticleManage = 7002,
        [Privilege("网站", "文章分类", 7003, "ArticleCategory/management", "articlecategory")]
        ArticleCategoryManage = 7003,


        ///*系统*/
        [Privilege("系统", "站点设置", 8001, "SiteSetting/Edit", "SiteSetting")]
        [Privilege("系统", "站点设置", 8001, "SiteSetting/Edit", "Navigation")]
        SiteSetting = 8001,
        [Privilege("系统", "管理员", 8002, "Manager/management", "Manager")]
        AdminManage = 8002,
        [Privilege("系统", "权限组", 8003, "Privilege/management", "privilege")]
        PrivilegesManage = 8003,
        [Privilege("系统", "操作日志", 8004, "OperationLog/management", "OperationLog")]
        OperationLog = 8004,
        [Privilege("系统", "消息设置", 8005, "Message/management", "Message")]
        MessageSetting = 8005,
        [Privilege("系统", "入驻设置", 8006, "Agreement/Settled", "Agreement")]
        Agreement = 8006,

        [Privilege("系统", "区域管理", 8007, "RegionArea/management", "RegionArea")]
        RegionArea = 8007,
        [Privilege("系统", "客服设置", 8008, "AdvancePayment/CustomerServicesEdit", "AdvancePayment")]
        ServiceSetting = 8008,

    }
}

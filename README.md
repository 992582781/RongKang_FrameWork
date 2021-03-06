# RongKang_FrameWork
框架主要特性如下：
1、系统整体使用Asp.net MVC+EF为主的多层架构，
各层通过Autofac进行构造函数依赖注入调用、降低系统各层之间带的耦合性（依赖于抽象，而不依赖于具体）；
2、用户管理权限开发，权限可以分配到数据级；主要包含模块管理、角色管理、权限配置、用户角色配置；
3、树形菜单插件开发，页面直接调用，只需配置请求URL地址与回显的数据；
4、数据提交验证插件开发，主要使用反射（Reflection）数据模型（特性中配置字段类型）对提交的字段进行验证；开发人员只需要调用数据验证函数，无需考虑具体验证细节；
5、所有的数据模型存入到缓存中，使用时直接从缓存中读取；
6、页面通过数据模型（Model）进行动态绑定显示，无需进行调整；可以在数据模型特性（Attribute）中配置控件类型（下拉框、文本框、数值框等）；
7、系统异常日志模块，通过使用IExceptionFilter进行全局日志系统设计，快速定位错误原因和具体代码位置；

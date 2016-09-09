# MeiQNetCheck

##MeiQNetCheck是一款用于网络检测的工具，采用C#开发，附带简洁的UI。

####目前支持的功能有：

#####1、检测本地网络是否能够接入Internet
#####2、检测本地NDS配置是否正确
#####3、通过Ping检测远程服务器的网络连通性
#####4、获取系统信息功能
#####5、邮件异步发送网络检测结果功能
#####6、窗口最小化到托盘，通过托盘进行退出（以便邮件在用户退出之前发完出）

####目前存在的问题：
#####~~1、程序在运行阶段会出现未响应的情况，导致用户体验差~~
    解决方式：将后台处理逻辑放到单独的线程中去做，由于存在跨线程访问组件的问题，这块通过委托并在后台线程中调用invoke。
#####~~2、存在内存泄漏~~
    解决方式：继承IDisposable接口，对非托管资源进行手动释放
#####~~3、获取系统信息与邮件异步发送网络检查的信息尚未组合到一起~~
    解决方式：已解决
#####4、目前只有新浪和网易的邮件服务器可以通过程序法送，QQ邮箱和QQ企业邮箱都不能通过程序发，而且网易邮件服务器会检测垃圾邮件，发送这种邮件成功率比较低，所以暂时选择新浪的邮件服务器来发邮件，QQ企业邮箱来收邮件，但是后续如果用程序爬QQ企业邮箱的邮件会不会出问题。。。
    
####总结：
#####1、代码在设计上存在一些不合理的地方，设计出的几个类有公共的部分，需要进行重构。
#####2、对于维护比较困难，每个模块内部可扩展性差。
#####3、各个模块之间的耦合度还是比较低的。

    

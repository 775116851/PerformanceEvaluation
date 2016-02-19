/*
 * FCKeditor - The text editor for Internet - http://www.fckeditor.net
 * Copyright (C) 2003-2010 Frederico Caldeira Knabben
 *
 * == BEGIN LICENSE ==
 *
 * Licensed under the terms of any of the following licenses at your
 * choice:
 *
 *  - GNU General Public License Version 2 or later (the "GPL")
 *    http://www.gnu.org/licenses/gpl.html
 *
 *  - GNU Lesser General Public License Version 2.1 or later (the "LGPL")
 *    http://www.gnu.org/licenses/lgpl.html
 *
 *  - Mozilla Public License Version 1.1 or later (the "MPL")
 *    http://www.mozilla.org/MPL/MPL-1.1.html
 *
 * == END LICENSE ==
 *
 * Editor configuration settings.
 *
 * Follow this link for more information:
 * http://docs.fckeditor.net/FCKeditor_2.x/Developers_Guide/Configuration/Configuration_Options
 */
 
// 自定义配置文件路径和名称
FCKConfig.CustomConfigurationsPath = '' ;
// 编辑区的样式表文件
FCKConfig.EditorAreaCSS = FCKConfig.BasePath + 'css/fck_editorarea.css';
// 编辑区的样式表风格
FCKConfig.EditorAreaStyles = '';
//工具栏预览CSS 
FCKConfig.ToolbarComboPreviewCSS = '' ;
//文档类型 
FCKConfig.DocType = '' ;
//相对链接的基地址
FCKConfig.BaseHref = '' ;
//是否允许编辑整个HTML文件,还是仅允许编辑BODY间的内容
FCKConfig.FullPage = false ;

// The following option determines whether the "Show Blocks" feature is enabled or not at startup.
FCKConfig.StartupShowBlocks = false ;
//是否开启调试功能
FCKConfig.Debug = false;
FCKConfig.AllowQueryStringDebug = true ;
//皮肤路径
FCKConfig.SkinPath = FCKConfig.BasePath + 'skins/default/';
FCKConfig.SkinEditorCSS = '' ;	// FCKConfig.SkinPath + "|<minified css>" ;
FCKConfig.SkinDialogCSS = '' ;	// FCKConfig.SkinPath + "|<minified css>" ;

//预装入的图片 
FCKConfig.PreloadImages = [ FCKConfig.SkinPath + 'images/toolbar.start.gif', FCKConfig.SkinPath + 'images/toolbar.buttonarrow.gif' ] ;
//插件路径
FCKConfig.PluginsPath = FCKConfig.BasePath + 'plugins/' ;

// FCKConfig.Plugins.Add( 'autogrow' ) ;
// FCKConfig.Plugins.Add( 'dragresizetable' );
// FCKConfig.AutoGrowMax = 400 ;

// FCKConfig.ProtectedSource.Add( /<%[\s\S]*?%>/g ) ;	// ASP style server side code <%...%>
// FCKConfig.ProtectedSource.Add( /<\?[\s\S]*?\?>/g ) ;	// PHP style server side code
// FCKConfig.ProtectedSource.Add( /(<asp:[^\>]+>[\s|\S]*?<\/asp:[^\>]+>)|(<asp:[^\>]+\/>)/gi ) ;	// ASP.Net style tags <asp:control>

//是否自动检测语言
FCKConfig.AutoDetectLanguage = false;
//默认语言
FCKConfig.DefaultLanguage = 'zh-cn';
//默认的文字方向,可选"ltr/rtl",即从左到右或从右到左 
FCKConfig.ContentLangDirection	= 'ltr' ;

FCKConfig.ProcessHTMLEntities = true; //处理HTML实体
FCKConfig.IncludeLatinEntities = true; //包括拉丁文
FCKConfig.IncludeGreekEntities = true; //包括希腊文

//处理数字实体
//原英文解释
//This option tells the editor to transform all characters that are out of the ASCII table to their relative Unicode numeric entities. 
//For example, if this option is set to true, the Ϣ sign will be transformed to &#994;. By default the option is set to false. 
FCKConfig.ProcessNumericEntities = false;

FCKConfig.AdditionalNumericEntities = ''; //附加的数字实体	// Single Quote: "'"

FCKConfig.FillEmptyBlocks = true; //是否填充空块

FCKConfig.FormatSource = true; //在切换到代码视图时是否自动格式化代码
FCKConfig.FormatOutput = true; //当输出内容时是否自动格式化代码
FCKConfig.FormatIndentator = '    '; //当在源码格式下缩进代码使用的字符

//They provide the ability to use some javascript in order to avoid bots that crawls the web pages to get email addresses. 
//避免网络爬虫抓取email信息
FCKConfig.EMailProtection = 'none' ; // none | encode | function
FCKConfig.EMailProtectionFunction = 'mt(NAME,DOMAIN,SUBJECT,BODY)' ;

FCKConfig.StartupFocus = false; //开启时焦点是否到编辑器,即打开页面时光标是否停留在fckeditor上
FCKConfig.ForcePasteAsPlainText = false; //是否强制粘贴为纯文件内容
FCKConfig.AutoDetectPasteFromWord = true; // IE only //是否自动探测从word粘贴文件,仅支持IE.
FCKConfig.ShowDropDialog = true; //是否显示下拉菜单
FCKConfig.ForceSimpleAmpersand = false; //是否不把&符号转换为XML实体
FCKConfig.TabSpaces = 0; //按下Tab键时光标跳格数,默认值为零为不跳格
FCKConfig.ShowBorders = true; //合并边框
FCKConfig.SourcePopup = false; //弹出
FCKConfig.ToolbarStartExpanded = true; //启动fckeditor工具栏默认是否展开
FCKConfig.ToolbarCanCollapse = true; //是否允许折叠或展开工具栏
FCKConfig.IgnoreEmptyParagraphValue = true; //是否忽略空的段落值
FCKConfig.FloatingPanelsZIndex = 10000; //浮动面板索引
FCKConfig.HtmlEncodeOutput = false; //是否将HTML编码输出

FCKConfig.TemplateReplaceAll = true; //是否替换所有模板
FCKConfig.TemplateReplaceCheckbox = true ;

FCKConfig.ToolbarLocation = 'In';  //工具栏位置,
//工具栏中的配置,具体含义可见鼠标放在工具栏上时的提示
//编辑器的工具栏，可以自行定义，删减，可参考已存在工具栏 
FCKConfig.ToolbarSets["Default"] = [
	['Source','DocProps','-','Save','NewPage','Preview','-','Templates'],
	['Cut','Copy','Paste','PasteText','PasteWord','-'/*,'Print','SpellCheck'*/],
	['Undo','Redo','-','Find','Replace','-','SelectAll','RemoveFormat'],
	//['Form','Checkbox','Radio','TextField','Textarea','Select','Button','ImageButton','HiddenField'],
	//'/',
	['Bold','Italic','Underline','StrikeThrough','-','Subscript','Superscript'],
	['OrderedList','UnorderedList','-','Outdent','Indent'/*,'Blockquote','CreateDiv'*/],
	['JustifyLeft','JustifyCenter','JustifyRight','JustifyFull'],
	['Link','Unlink','Anchor'],
	['Image'/*,'Flash'*/,'Table','Rule'/*,'Smiley','SpecialChar','PageBreak'*/],
	//'/',
	['Style','FontFormat','FontName','FontSize'],
	['TextColor','BGColor'],
	['FitWindow','ShowBlocks','-'/*,'About'*/]		// No comma for the last row.
] ;

FCKConfig.ToolbarSets["Basic"] = [
	['Bold','Italic','-','OrderedList','UnorderedList','-','Link','Unlink','-','About']
];

// 编辑器中直接回车，在代码中生成，可选为p | div | br    
FCKConfig.EnterMode = 'p'; 		// p | div | br
FCKConfig.ShiftEnterMode = 'br' ;	// p | div | br

//快捷键
FCKConfig.Keystrokes = [
	[ CTRL + 65 /*A*/, true ],
	[ CTRL + 67 /*C*/, true ],
	[ CTRL + 70 /*F*/, true ],
	[ CTRL + 83 /*S*/, true ],
	[ CTRL + 84 /*T*/, true ],
	[ CTRL + 88 /*X*/, true ],
	[ CTRL + 86 /*V*/, 'Paste' ],
	[ CTRL + 45 /*INS*/, true ],
	[ SHIFT + 45 /*INS*/, 'Paste' ],
	[ CTRL + 88 /*X*/, 'Cut' ],
	[ SHIFT + 46 /*DEL*/, 'Cut' ],
	[ CTRL + 90 /*Z*/, 'Undo' ],
	[ CTRL + 89 /*Y*/, 'Redo' ],
	[ CTRL + SHIFT + 90 /*Z*/, 'Redo' ],
	[ CTRL + 76 /*L*/, 'Link' ],
	[ CTRL + 66 /*B*/, 'Bold' ],
	[ CTRL + 73 /*I*/, 'Italic' ],
	[ CTRL + 85 /*U*/, 'Underline' ],
	[ CTRL + SHIFT + 83 /*S*/, 'Save' ],
	[ CTRL + ALT + 13 /*ENTER*/, 'FitWindow' ],
	[ SHIFT + 32 /*SPACE*/, 'Nbsp' ]
] ;

// 右键菜单的内容
FCKConfig.ContextMenu = ['Generic','Link','Anchor','Image','Flash','Select','Textarea','Checkbox','Radio','TextField','HiddenField','ImageButton','Button','BulletedList','NumberedList','Table','Form','DivContainer'] ;
//This option, if set to true makes it possible to display the default browser's context menu when right-clicking with the CTRL key pressed. 
FCKConfig.BrowserContextMenuOnCtrl = false ;
FCKConfig.BrowserContextMenu = false ;

//颜色
FCKConfig.EnableMoreFontColors = true ;
FCKConfig.FontColors = '000000,993300,333300,003300,003366,000080,333399,333333,800000,FF6600,808000,808080,008080,0000FF,666699,808080,FF0000,FF9900,99CC00,339966,33CCCC,3366FF,800080,999999,FF00FF,FFCC00,FFFF00,00FF00,00FFFF,00CCFF,993366,C0C0C0,FF99CC,FFCC99,FFFF99,CCFFCC,CCFFFF,99CCFF,CC99FF,FFFFFF' ;

//字体,样式等
FCKConfig.FontFormats	= 'p;h1;h2;h3;h4;h5;h6;pre;address;div' ;
//FCKConfig.FontNames		= 'Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana' ;
//FCKConfig.FontSizes		= 'smaller;larger;xx-small;x-small;small;medium;large;x-large;xx-large' ;
FCKConfig.FontNames		= 'Arial;Comic Sans MS;Courier New;Tahoma;Times New Roman;Verdana;宋体;楷体;隶书;微软雅黑;仿宋' ;
FCKConfig.FontSizes = '8;9;10;11;12;13;14;16;18;20;22;24;26;28;36;48;72';
FCKConfig.StylesXmlPath = FCKConfig.EditorPath + 'fckstyles.xml'; // CSS样式列表的XML文件的位置
FCKConfig.TemplatesXmlPath = FCKConfig.EditorPath + 'fcktemplates.xml'; // 模版的XML文件位置

//拼写检查器
FCKConfig.SpellChecker			= 'WSC' ;	// 'WSC' | 'SCAYT' | 'SpellerPages' | 'ieSpell'
FCKConfig.IeSpellDownloadUrl	= 'http://www.iespell.com/download.php' ;
FCKConfig.SpellerPagesServerScript = 'server-scripts/spellchecker.php' ;	// Available extension: .php .cfm .pl
FCKConfig.FirefoxSpellChecker	= false ;

//最大的Undo步数
FCKConfig.MaxUndoLevels = 15 ;
//This option enables/disables the ability to resize objects in the editing area, like images and tables. By default the option is set to 'false' so objects can be resized.
FCKConfig.DisableObjectResizing = false; //是否禁止用户调整图像和表格的大小
FCKConfig.DisableFFTableHandles = true; //是否禁用表格工具

FCKConfig.LinkDlgHideTarget = false; //是否隐藏Link窗口的target标签
FCKConfig.LinkDlgHideAdvanced = false; //是否隐藏Link窗口的advanced标签

FCKConfig.ImageDlgHideLink = false; //是否隐藏image窗口的link标签
FCKConfig.ImageDlgHideAdvanced = false; //是否隐藏image窗口的advanced标签

FCKConfig.FlashDlgHideAdvanced = false; //是否隐藏Flash窗口的advanced标签

FCKConfig.ProtectedTags = ''; //添加HTML套用格式

// This will be applied to the body element of the editor
FCKConfig.BodyId = ''; //设置编辑器的id
FCKConfig.BodyClass = '';  //设置编辑器的class

FCKConfig.DefaultStyleLabel = ''; //设置文本编辑器的风格，默认为空白文档
FCKConfig.DefaultFontFormatLabel = ''; //设置默认格式
FCKConfig.DefaultFontLabel = ''; //设置默认字体
FCKConfig.DefaultFontSizeLabel = ''; //设置默认字体大小
FCKConfig.DefaultLinkTarget = ''; //设置默认链接目标为(_blank、_self _parent、_top)

// The option switches between trying to keep the html structure or do the changes so the content looks like it was in Word
FCKConfig.CleanWordKeepsStructure = false; //是否设置直接粘贴为Word格式

// Only inline elements are valid.
//删除文字时是否删除相应的格式
FCKConfig.RemoveFormatTags = 'b,big,code,del,dfn,em,font,i,ins,kbd,q,samp,small,span,strike,strong,sub,sup,tt,u,var' ;

// Attributes that will be removed
//删除文字时是否删除相应的样式
FCKConfig.RemoveAttributes = 'class,style,lang,width,height,align,hspace,valign';

//样式菜单
FCKConfig.CustomStyles =
{
	'Red Title'	: { Element : 'h3', Styles : { 'color' : 'Red' } }
};

// Do not add, rename or remove styles here. Only apply definition changes.
//设置FCKeditor核心样式
FCKConfig.CoreStyles =
{
	// Basic Inline Styles.
	'Bold'			: { Element : 'strong', Overrides : 'b' },
	'Italic'		: { Element : 'em', Overrides : 'i' },
	'Underline'		: { Element : 'u' },
	'StrikeThrough'	: { Element : 'strike' },
	'Subscript'		: { Element : 'sub' },
	'Superscript'	: { Element : 'sup' },

	// Basic Block Styles (Font Format Combo).
	'p'				: { Element : 'p' },
	'div'			: { Element : 'div' },
	'pre'			: { Element : 'pre' },
	'address'		: { Element : 'address' },
	'h1'			: { Element : 'h1' },
	'h2'			: { Element : 'h2' },
	'h3'			: { Element : 'h3' },
	'h4'			: { Element : 'h4' },
	'h5'			: { Element : 'h5' },
	'h6'			: { Element : 'h6' },

	// Other formatting features.
	'FontFace' :
	{
		Element		: 'span',
		Styles		: { 'font-family' : '#("Font")' },
		Overrides	: [ { Element : 'font', Attributes : { 'face' : null } } ]
	},

	'Size' :
	{
		Element		: 'span',
		Styles		: { 'font-size' : '#("Size","fontSize")' },
		Overrides	: [ { Element : 'font', Attributes : { 'size' : null } } ]
	},

	'Color' :
	{
		Element		: 'span',
		Styles		: { 'color' : '#("Color","color")' },
		Overrides	: [ { Element : 'font', Attributes : { 'color' : null } } ]
	},

	'BackColor'		: { Element : 'span', Styles : { 'background-color' : '#("Color","color")' } },

	'SelectionHighlight' : { Element : 'span', Styles : { 'background-color' : 'navy', 'color' : 'white' } }
};

// The distance of an indentation step.
//编辑器中缩进量的长度
FCKConfig.IndentLength = 40;
//编辑器中缩进量的单位
FCKConfig.IndentUnit = 'px';

// Alternatively, FCKeditor allows the use of CSS classes for block indentation.
// This overrides the IndentLength/IndentUnit settings.
//FCKeditor允许使用CSS缩进
FCKConfig.IndentClasses = [];

// [ Left, Center, Right, Justified ]
//FCKeditor允许使用CSS类文本
FCKConfig.JustifyClasses = [];

// The following value defines which File Browser connector and Quick Upload
// "uploader" to use. It is valid for the default implementaion and it is here
// just to make this configuration file cleaner.
// It is not possible to change this value using an external file or even
// inline when creating the editor instance. In that cases you must set the
// values of LinkBrowserURL, ImageBrowserURL and so on.
// Custom implementations should just ignore it.
//文件浏览器使用的语言
var _FileBrowserLanguage = 'aspx'; // asp | aspx | cfm | lasso | perl | php | py
//快速上传使用的语言
var _QuickUploadLanguage = 'aspx'; // asp | aspx | cfm | lasso | perl | php | py

// Don't care about the following two lines. It just calculates the correct connector
// extension to use for the default File Browser (Perl uses "cgi").
//文件浏览器扩展
var _FileBrowserExtension = _FileBrowserLanguage == 'perl' ? 'cgi' : _FileBrowserLanguage;
//快速上传扩展
var _QuickUploadExtension = _QuickUploadLanguage == 'perl' ? 'cgi' : _QuickUploadLanguage;

//是否允许在插入链接时浏览服务器
FCKConfig.LinkBrowser = true;
//插入链接时浏览服务器的URL
FCKConfig.LinkBrowserURL = FCKConfig.BasePath + 'filemanager/browser/default/browser.html?Connector=' + encodeURIComponent(FCKConfig.BasePath + 'filemanager/connectors/' + _FileBrowserLanguage + '/connector.' + _FileBrowserExtension);
//链接目标浏览器窗口宽度
FCKConfig.LinkBrowserWindowWidth = FCKConfig.ScreenWidth * 0.7; 	// 70%
//链接目标浏览器窗口高度
FCKConfig.LinkBrowserWindowHeight = FCKConfig.ScreenHeight * 0.7; // 70%

//是否关闭图片文件浏览服务器的功能
FCKConfig.ImageBrowser = true;
//图片文件浏览服务器的URL
FCKConfig.ImageBrowserURL = FCKConfig.BasePath + 'filemanager/browser/default/browser.html?Type=Image&Connector=' + encodeURIComponent(FCKConfig.BasePath + 'filemanager/connectors/' + _FileBrowserLanguage + '/connector.' + _FileBrowserExtension);
//图像浏览器窗口宽度
FCKConfig.ImageBrowserWindowWidth = FCKConfig.ScreenWidth * 0.7; // 70% ;
//图像浏览器窗口高度
FCKConfig.ImageBrowserWindowHeight = FCKConfig.ScreenHeight * 0.7; // 70% ;

//是否关闭Flash浏览服务器的功能
FCKConfig.FlashBrowser = true;
FCKConfig.FlashBrowserURL = FCKConfig.BasePath + 'filemanager/browser/default/browser.html?Type=Flash&Connector=' + encodeURIComponent( FCKConfig.BasePath + 'filemanager/connectors/' + _FileBrowserLanguage + '/connector.' + _FileBrowserExtension ) ;
FCKConfig.FlashBrowserWindowWidth  = FCKConfig.ScreenWidth * 0.7 ;	//70% ;
FCKConfig.FlashBrowserWindowHeight = FCKConfig.ScreenHeight * 0.7 ;	//70% ;

//是否开启文件上传的功能
FCKConfig.LinkUpload = true;
FCKConfig.LinkUploadURL = FCKConfig.BasePath + 'filemanager/connectors/' + _QuickUploadLanguage + '/upload.' + _QuickUploadExtension ;
FCKConfig.LinkUploadAllowedExtensions	= ".(7z|aiff|asf|avi|bmp|csv|doc|fla|flv|gif|gz|gzip|jpeg|jpg|mid|mov|mp3|mp4|mpc|mpeg|mpg|ods|odt|pdf|png|ppt|pxd|qt|ram|rar|rm|rmi|rmvb|rtf|sdc|sitd|swf|sxc|sxw|tar|tgz|tif|tiff|txt|vsd|wav|wma|wmv|xls|xml|zip)$" ;			// empty for all
FCKConfig.LinkUploadDeniedExtensions	= "" ;	// empty for no one

//是否开启图片上传功能
FCKConfig.ImageUpload = true;
FCKConfig.ImageUploadURL = FCKConfig.BasePath + 'filemanager/connectors/' + _QuickUploadLanguage + '/upload.' + _QuickUploadExtension + '?Type=Image' ;
FCKConfig.ImageUploadAllowedExtensions = ".(jpg|gif|jpeg|png)$"; 	// empty for all
//This option specifies the image upload extensions which you don't wish to use in FCKeditor
FCKConfig.ImageUploadDeniedExtensions	= "" ;							// empty for no one

FCKConfig.FlashUpload = true ;
FCKConfig.FlashUploadURL = FCKConfig.BasePath + 'filemanager/connectors/' + _QuickUploadLanguage + '/upload.' + _QuickUploadExtension + '?Type=Flash' ;
FCKConfig.FlashUploadAllowedExtensions	= ".(swf|flv)$" ;		// empty for all
FCKConfig.FlashUploadDeniedExtensions	= "" ;					// empty for no one

//插入表情图标的路径
FCKConfig.SmileyPath = FCKConfig.BasePath + 'images/smiley/msn/';
//表情图标的文件名称
FCKConfig.SmileyImages = ['regular_smile.gif', 'sad_smile.gif', 'wink_smile.gif', 'teeth_smile.gif', 'confused_smile.gif', 'tounge_smile.gif', 'embaressed_smile.gif', 'omg_smile.gif', 'whatchutalkingabout_smile.gif', 'angry_smile.gif', 'angel_smile.gif', 'shades_smile.gif', 'devil_smile.gif', 'cry_smile.gif', 'lightbulb.gif', 'thumbs_down.gif', 'thumbs_up.gif', 'heart.gif', 'broken_heart.gif', 'kiss.gif', 'envelope.gif'];
//表情窗口显示表情列数
FCKConfig.SmileyColumns = 8;
FCKConfig.SmileyWindowWidth		= 320 ;
FCKConfig.SmileyWindowHeight	= 210 ;
//编辑器弹出窗口时，背景遮照住的颜色
FCKConfig.BackgroundBlockerColor = '#ffffff';
//编辑器弹出窗口时，背景遮照住的透明度
FCKConfig.BackgroundBlockerOpacity = 0.50;

FCKConfig.MsWebBrowserControlCompat = false ;

FCKConfig.PreventSubmitHandler = false ;






<!DOCTYPE html>
<html class=" ">
  <head prefix="og: http://ogp.me/ns# fb: http://ogp.me/ns/fb# object: http://ogp.me/ns/object# article: http://ogp.me/ns/article# profile: http://ogp.me/ns/profile#">
    <meta charset='utf-8'>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    
    
    <title>jquery-scrollto/src/documents/lib/jquery-scrollto.js at master · balupton/jquery-scrollto</title>
    <link rel="search" type="application/opensearchdescription+xml" href="/opensearch.xml" title="GitHub" />
    <link rel="fluid-icon" href="https://github.com/fluidicon.png" title="GitHub" />
    <link rel="apple-touch-icon" sizes="57x57" href="/apple-touch-icon-114.png" />
    <link rel="apple-touch-icon" sizes="114x114" href="/apple-touch-icon-114.png" />
    <link rel="apple-touch-icon" sizes="72x72" href="/apple-touch-icon-144.png" />
    <link rel="apple-touch-icon" sizes="144x144" href="/apple-touch-icon-144.png" />
    <meta property="fb:app_id" content="1401488693436528"/>

      <meta content="@github" name="twitter:site" /><meta content="summary" name="twitter:card" /><meta content="balupton/jquery-scrollto" name="twitter:title" /><meta content="jquery-scrollto - jQuery ScrollTo - Smooth Scrolling to any jQuery/DOM Element" name="twitter:description" /><meta content="https://avatars1.githubusercontent.com/u/61148?s=400" name="twitter:image:src" />
<meta content="GitHub" property="og:site_name" /><meta content="object" property="og:type" /><meta content="https://avatars1.githubusercontent.com/u/61148?s=400" property="og:image" /><meta content="balupton/jquery-scrollto" property="og:title" /><meta content="https://github.com/balupton/jquery-scrollto" property="og:url" /><meta content="jquery-scrollto - jQuery ScrollTo - Smooth Scrolling to any jQuery/DOM Element" property="og:description" />

    <link rel="assets" href="https://github.global.ssl.fastly.net/">
    <link rel="conduit-xhr" href="https://ghconduit.com:25035/">
    <link rel="xhr-socket" href="/_sockets" />


    <meta name="msapplication-TileImage" content="/windows-tile.png" />
    <meta name="msapplication-TileColor" content="#ffffff" />
    <meta name="selected-link" value="repo_source" data-pjax-transient />
    <meta content="collector.githubapp.com" name="octolytics-host" /><meta content="collector-cdn.github.com" name="octolytics-script-host" /><meta content="github" name="octolytics-app-id" /><meta content="B194F8AA:1342:1E06E7D:532A3BAD" name="octolytics-dimension-request_id" /><meta content="709210" name="octolytics-actor-id" /><meta content="btachinardi" name="octolytics-actor-login" /><meta content="e7960e6dca988fb37b2bb0fec1ef2cab4c2f86b39f0af55e3490655b2d0c1e99" name="octolytics-actor-hash" />
    

    
    
    <link rel="icon" type="image/x-icon" href="https://github.global.ssl.fastly.net/favicon.ico" />

    <meta content="authenticity_token" name="csrf-param" />
<meta content="PY/viUjtnTeRqSIYHJVRIsPwgfFhB7JyBuMFIOCA+GQ=" name="csrf-token" />

    <link href="https://github.global.ssl.fastly.net/assets/github-4d35cd1209db5e03d9a5175b09b6e3d92bfa58bc.css" media="all" rel="stylesheet" type="text/css" />
    <link href="https://github.global.ssl.fastly.net/assets/github2-a74475ad543ac6f8b56cb712501559c94544d132.css" media="all" rel="stylesheet" type="text/css" />
    


        <script crossorigin="anonymous" src="https://github.global.ssl.fastly.net/assets/frameworks-40c107d5f9c17b1c5a24d77604a4722218ebdadd.js" type="text/javascript"></script>
        <script async="async" crossorigin="anonymous" src="https://github.global.ssl.fastly.net/assets/github-06a43cc0a86515267e7812df5298cdef18b25c10.js" type="text/javascript"></script>
        
        
      <meta http-equiv="x-pjax-version" content="d5496792e6a11f774f0fc7221e6b42fb">

        <link data-pjax-transient rel='permalink' href='/balupton/jquery-scrollto/blob/791cc3157956e1319c32aa14d5eaa5fd6bae379f/src/documents/lib/jquery-scrollto.js'>

  <meta name="description" content="jquery-scrollto - jQuery ScrollTo - Smooth Scrolling to any jQuery/DOM Element" />

  <meta content="61148" name="octolytics-dimension-user_id" /><meta content="balupton" name="octolytics-dimension-user_login" /><meta content="867973" name="octolytics-dimension-repository_id" /><meta content="balupton/jquery-scrollto" name="octolytics-dimension-repository_nwo" /><meta content="true" name="octolytics-dimension-repository_public" /><meta content="false" name="octolytics-dimension-repository_is_fork" /><meta content="867973" name="octolytics-dimension-repository_network_root_id" /><meta content="balupton/jquery-scrollto" name="octolytics-dimension-repository_network_root_nwo" />
  <link href="https://github.com/balupton/jquery-scrollto/commits/master.atom" rel="alternate" title="Recent Commits to jquery-scrollto:master" type="application/atom+xml" />

  </head>


  <body class="logged_in  env-production windows vis-public page-blob">
    <a href="#start-of-content" class="accessibility-aid js-skip-to-content">Skip to content</a>
    <div class="wrapper">
      
      
      
      


      <div class="header header-logged-in true">
  <div class="container clearfix">

    <a class="header-logo-invertocat" href="https://github.com/">
  <span class="mega-octicon octicon-mark-github"></span>
</a>

    
    <a href="/notifications" aria-label="You have no unread notifications" class="notification-indicator tooltipped tooltipped-s" data-gotokey="n">
        <span class="mail-status all-read"></span>
</a>

      <div class="command-bar js-command-bar  in-repository">
          <form accept-charset="UTF-8" action="/search" class="command-bar-form" id="top_search_form" method="get">

<input type="text" data-hotkey="/ s" name="q" id="js-command-bar-field" placeholder="Search or type a command" tabindex="1" autocapitalize="off"
    
    data-username="btachinardi"
      data-repo="balupton/jquery-scrollto"
      data-branch="master"
      data-sha="a36e9d03a009b22fcdb10109b7584995016aa4ed"
  >

    <input type="hidden" name="nwo" value="balupton/jquery-scrollto" />

    <div class="select-menu js-menu-container js-select-menu search-context-select-menu">
      <span class="minibutton select-menu-button js-menu-target" role="button" aria-haspopup="true">
        <span class="js-select-button">This repository</span>
      </span>

      <div class="select-menu-modal-holder js-menu-content js-navigation-container" aria-hidden="true">
        <div class="select-menu-modal">

          <div class="select-menu-item js-navigation-item js-this-repository-navigation-item selected">
            <span class="select-menu-item-icon octicon octicon-check"></span>
            <input type="radio" class="js-search-this-repository" name="search_target" value="repository" checked="checked" />
            <div class="select-menu-item-text js-select-button-text">This repository</div>
          </div> <!-- /.select-menu-item -->

          <div class="select-menu-item js-navigation-item js-all-repositories-navigation-item">
            <span class="select-menu-item-icon octicon octicon-check"></span>
            <input type="radio" name="search_target" value="global" />
            <div class="select-menu-item-text js-select-button-text">All repositories</div>
          </div> <!-- /.select-menu-item -->

        </div>
      </div>
    </div>

  <span class="help tooltipped tooltipped-s" aria-label="Show command bar help">
    <span class="octicon octicon-question"></span>
  </span>


  <input type="hidden" name="ref" value="cmdform">

</form>
        <ul class="top-nav">
          <li class="explore"><a href="/explore">Explore</a></li>
            <li><a href="https://gist.github.com">Gist</a></li>
            <li><a href="/blog">Blog</a></li>
          <li><a href="https://help.github.com">Help</a></li>
        </ul>
      </div>

    


  <ul id="user-links">
    <li>
      <a href="/btachinardi" class="name">
        <img alt="btachinardi" class=" js-avatar" data-user="709210" height="20" src="https://avatars3.githubusercontent.com/u/709210?s=140" width="20" /> btachinardi
      </a>
    </li>

    <li class="new-menu dropdown-toggle js-menu-container">
      <a href="#" class="js-menu-target tooltipped tooltipped-s" aria-label="Create new...">
        <span class="octicon octicon-plus"></span>
        <span class="dropdown-arrow"></span>
      </a>

      <div class="new-menu-content js-menu-content">
      </div>
    </li>

    <li>
      <a href="/settings/profile" id="account_settings"
        class="tooltipped tooltipped-s"
        aria-label="Account settings ">
        <span class="octicon octicon-tools"></span>
      </a>
    </li>
    <li>
      <a class="tooltipped tooltipped-s" href="/logout" data-method="post" id="logout" aria-label="Sign out">
        <span class="octicon octicon-log-out"></span>
      </a>
    </li>

  </ul>

<div class="js-new-dropdown-contents hidden">
  

<ul class="dropdown-menu">
  <li>
    <a href="/new"><span class="octicon octicon-repo-create"></span> New repository</a>
  </li>
  <li>
    <a href="/organizations/new"><span class="octicon octicon-organization"></span> New organization</a>
  </li>


    <li class="section-title">
      <span title="balupton/jquery-scrollto">This repository</span>
    </li>
      <li>
        <a href="/balupton/jquery-scrollto/issues/new"><span class="octicon octicon-issue-opened"></span> New issue</a>
      </li>
</ul>

</div>


    
  </div>
</div>

      

        



      <div id="start-of-content" class="accessibility-aid"></div>
          <div class="site" itemscope itemtype="http://schema.org/WebPage">
    
    <div class="pagehead repohead instapaper_ignore readability-menu">
      <div class="container">
        

<ul class="pagehead-actions">

    <li class="subscription">
      <form accept-charset="UTF-8" action="/notifications/subscribe" class="js-social-container" data-autosubmit="true" data-remote="true" method="post"><div style="margin:0;padding:0;display:inline"><input name="authenticity_token" type="hidden" value="PY/viUjtnTeRqSIYHJVRIsPwgfFhB7JyBuMFIOCA+GQ=" /></div>  <input id="repository_id" name="repository_id" type="hidden" value="867973" />

    <div class="select-menu js-menu-container js-select-menu">
      <a class="social-count js-social-count" href="/balupton/jquery-scrollto/watchers">
        25
      </a>
      <span class="minibutton select-menu-button with-count js-menu-target" role="button" tabindex="0" aria-haspopup="true">
        <span class="js-select-button">
          <span class="octicon octicon-eye-watch"></span>
          Watch
        </span>
      </span>

      <div class="select-menu-modal-holder">
        <div class="select-menu-modal subscription-menu-modal js-menu-content" aria-hidden="true">
          <div class="select-menu-header">
            <span class="select-menu-title">Notification status</span>
            <span class="octicon octicon-remove-close js-menu-close"></span>
          </div> <!-- /.select-menu-header -->

          <div class="select-menu-list js-navigation-container" role="menu">

            <div class="select-menu-item js-navigation-item selected" role="menuitem" tabindex="0">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <div class="select-menu-item-text">
                <input checked="checked" id="do_included" name="do" type="radio" value="included" />
                <h4>Not watching</h4>
                <span class="description">You only receive notifications for conversations in which you participate or are @mentioned.</span>
                <span class="js-select-button-text hidden-select-button-text">
                  <span class="octicon octicon-eye-watch"></span>
                  Watch
                </span>
              </div>
            </div> <!-- /.select-menu-item -->

            <div class="select-menu-item js-navigation-item " role="menuitem" tabindex="0">
              <span class="select-menu-item-icon octicon octicon octicon-check"></span>
              <div class="select-menu-item-text">
                <input id="do_subscribed" name="do" type="radio" value="subscribed" />
                <h4>Watching</h4>
                <span class="description">You receive notifications for all conversations in this repository.</span>
                <span class="js-select-button-text hidden-select-button-text">
                  <span class="octicon octicon-eye-unwatch"></span>
                  Unwatch
                </span>
              </div>
            </div> <!-- /.select-menu-item -->

            <div class="select-menu-item js-navigation-item " role="menuitem" tabindex="0">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <div class="select-menu-item-text">
                <input id="do_ignore" name="do" type="radio" value="ignore" />
                <h4>Ignoring</h4>
                <span class="description">You do not receive any notifications for conversations in this repository.</span>
                <span class="js-select-button-text hidden-select-button-text">
                  <span class="octicon octicon-mute"></span>
                  Stop ignoring
                </span>
              </div>
            </div> <!-- /.select-menu-item -->

          </div> <!-- /.select-menu-list -->

        </div> <!-- /.select-menu-modal -->
      </div> <!-- /.select-menu-modal-holder -->
    </div> <!-- /.select-menu -->

</form>
    </li>

  <li>
  

  <div class="js-toggler-container js-social-container starring-container ">
    <a href="/balupton/jquery-scrollto/unstar"
      class="minibutton with-count js-toggler-target star-button starred"
      aria-label="Unstar this repository" title="Unstar balupton/jquery-scrollto" data-remote="true" data-method="post" rel="nofollow">
      <span class="octicon octicon-star-delete"></span><span class="text">Unstar</span>
    </a>

    <a href="/balupton/jquery-scrollto/star"
      class="minibutton with-count js-toggler-target star-button unstarred"
      aria-label="Star this repository" title="Star balupton/jquery-scrollto" data-remote="true" data-method="post" rel="nofollow">
      <span class="octicon octicon-star"></span><span class="text">Star</span>
    </a>

      <a class="social-count js-social-count" href="/balupton/jquery-scrollto/stargazers">
        269
      </a>
  </div>

  </li>


        <li>
          <a href="/balupton/jquery-scrollto/fork" class="minibutton with-count js-toggler-target fork-button lighter tooltipped-n" title="Fork your own copy of balupton/jquery-scrollto to your account" aria-label="Fork your own copy of balupton/jquery-scrollto to your account" rel="facebox nofollow">
            <span class="octicon octicon-git-branch-create"></span><span class="text">Fork</span>
          </a>
          <a href="/balupton/jquery-scrollto/network" class="social-count">58</a>
        </li>


</ul>

        <h1 itemscope itemtype="http://data-vocabulary.org/Breadcrumb" class="entry-title public">
          <span class="repo-label"><span>public</span></span>
          <span class="mega-octicon octicon-repo"></span>
          <span class="author">
            <a href="/balupton" class="url fn" itemprop="url" rel="author"><span itemprop="title">balupton</span></a>
          </span>
          <span class="repohead-name-divider">/</span>
          <strong><a href="/balupton/jquery-scrollto" class="js-current-repository js-repo-home-link">jquery-scrollto</a></strong>

          <span class="page-context-loader">
            <img alt="Octocat-spinner-32" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
          </span>

        </h1>
      </div><!-- /.container -->
    </div><!-- /.repohead -->

    <div class="container">
      <div class="repository-with-sidebar repo-container new-discussion-timeline js-new-discussion-timeline  ">
        <div class="repository-sidebar clearfix">
            

<div class="sunken-menu vertical-right repo-nav js-repo-nav js-repository-container-pjax js-octicon-loaders">
  <div class="sunken-menu-contents">
    <ul class="sunken-menu-group">
      <li class="tooltipped tooltipped-w" aria-label="Code">
        <a href="/balupton/jquery-scrollto" aria-label="Code" class="selected js-selected-navigation-item sunken-menu-item" data-gotokey="c" data-pjax="true" data-selected-links="repo_source repo_downloads repo_commits repo_tags repo_branches /balupton/jquery-scrollto">
          <span class="octicon octicon-code"></span> <span class="full-word">Code</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

        <li class="tooltipped tooltipped-w" aria-label="Issues">
          <a href="/balupton/jquery-scrollto/issues" aria-label="Issues" class="js-selected-navigation-item sunken-menu-item js-disable-pjax" data-gotokey="i" data-selected-links="repo_issues /balupton/jquery-scrollto/issues">
            <span class="octicon octicon-issue-opened"></span> <span class="full-word">Issues</span>
            <span class='counter'>8</span>
            <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>        </li>

      <li class="tooltipped tooltipped-w" aria-label="Pull Requests">
        <a href="/balupton/jquery-scrollto/pulls" aria-label="Pull Requests" class="js-selected-navigation-item sunken-menu-item js-disable-pjax" data-gotokey="p" data-selected-links="repo_pulls /balupton/jquery-scrollto/pulls">
            <span class="octicon octicon-git-pull-request"></span> <span class="full-word">Pull Requests</span>
            <span class='counter'>2</span>
            <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>


        <li class="tooltipped tooltipped-w" aria-label="Wiki">
          <a href="/balupton/jquery-scrollto/wiki" aria-label="Wiki" class="js-selected-navigation-item sunken-menu-item" data-pjax="true" data-selected-links="repo_wiki /balupton/jquery-scrollto/wiki">
            <span class="octicon octicon-book"></span> <span class="full-word">Wiki</span>
            <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>        </li>
    </ul>
    <div class="sunken-menu-separator"></div>
    <ul class="sunken-menu-group">

      <li class="tooltipped tooltipped-w" aria-label="Pulse">
        <a href="/balupton/jquery-scrollto/pulse" aria-label="Pulse" class="js-selected-navigation-item sunken-menu-item" data-pjax="true" data-selected-links="pulse /balupton/jquery-scrollto/pulse">
          <span class="octicon octicon-pulse"></span> <span class="full-word">Pulse</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

      <li class="tooltipped tooltipped-w" aria-label="Graphs">
        <a href="/balupton/jquery-scrollto/graphs" aria-label="Graphs" class="js-selected-navigation-item sunken-menu-item" data-pjax="true" data-selected-links="repo_graphs repo_contributors /balupton/jquery-scrollto/graphs">
          <span class="octicon octicon-graph"></span> <span class="full-word">Graphs</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>

      <li class="tooltipped tooltipped-w" aria-label="Network">
        <a href="/balupton/jquery-scrollto/network" aria-label="Network" class="js-selected-navigation-item sunken-menu-item js-disable-pjax" data-selected-links="repo_network /balupton/jquery-scrollto/network">
          <span class="octicon octicon-git-branch"></span> <span class="full-word">Network</span>
          <img alt="Octocat-spinner-32" class="mini-loader" height="16" src="https://github.global.ssl.fastly.net/images/spinners/octocat-spinner-32.gif" width="16" />
</a>      </li>
    </ul>


  </div>
</div>

              <div class="only-with-full-nav">
                

  

<div class="clone-url open"
  data-protocol-type="http"
  data-url="/users/set_protocol?protocol_selector=http&amp;protocol_type=clone">
  <h3><strong>HTTPS</strong> clone URL</h3>
  <div class="clone-url-box">
    <input type="text" class="clone js-url-field"
           value="https://github.com/balupton/jquery-scrollto.git" readonly="readonly">

    <span aria-label="copy to clipboard" class="js-zeroclipboard url-box-clippy minibutton zeroclipboard-button" data-clipboard-text="https://github.com/balupton/jquery-scrollto.git" data-copied-hint="copied!"><span class="octicon octicon-clippy"></span></span>
  </div>
</div>

  

<div class="clone-url "
  data-protocol-type="ssh"
  data-url="/users/set_protocol?protocol_selector=ssh&amp;protocol_type=clone">
  <h3><strong>SSH</strong> clone URL</h3>
  <div class="clone-url-box">
    <input type="text" class="clone js-url-field"
           value="git@github.com:balupton/jquery-scrollto.git" readonly="readonly">

    <span aria-label="copy to clipboard" class="js-zeroclipboard url-box-clippy minibutton zeroclipboard-button" data-clipboard-text="git@github.com:balupton/jquery-scrollto.git" data-copied-hint="copied!"><span class="octicon octicon-clippy"></span></span>
  </div>
</div>

  

<div class="clone-url "
  data-protocol-type="subversion"
  data-url="/users/set_protocol?protocol_selector=subversion&amp;protocol_type=clone">
  <h3><strong>Subversion</strong> checkout URL</h3>
  <div class="clone-url-box">
    <input type="text" class="clone js-url-field"
           value="https://github.com/balupton/jquery-scrollto" readonly="readonly">

    <span aria-label="copy to clipboard" class="js-zeroclipboard url-box-clippy minibutton zeroclipboard-button" data-clipboard-text="https://github.com/balupton/jquery-scrollto" data-copied-hint="copied!"><span class="octicon octicon-clippy"></span></span>
  </div>
</div>


<p class="clone-options">You can clone with
      <a href="#" class="js-clone-selector" data-protocol="http">HTTPS</a>,
      <a href="#" class="js-clone-selector" data-protocol="ssh">SSH</a>,
      or <a href="#" class="js-clone-selector" data-protocol="subversion">Subversion</a>.
  <span class="help tooltipped tooltipped-n" aria-label="Get help on which URL is right for you.">
    <a href="https://help.github.com/articles/which-remote-url-should-i-use">
    <span class="octicon octicon-question"></span>
    </a>
  </span>
</p>


  <a href="http://windows.github.com" class="minibutton sidebar-button" title="Save balupton/jquery-scrollto to your computer and use it in GitHub Desktop." aria-label="Save balupton/jquery-scrollto to your computer and use it in GitHub Desktop.">
    <span class="octicon octicon-device-desktop"></span>
    Clone in Desktop
  </a>

                <a href="/balupton/jquery-scrollto/archive/master.zip"
                   class="minibutton sidebar-button"
                   aria-label="Download balupton/jquery-scrollto as a zip file"
                   title="Download balupton/jquery-scrollto as a zip file"
                   rel="nofollow">
                  <span class="octicon octicon-cloud-download"></span>
                  Download ZIP
                </a>
              </div>
        </div><!-- /.repository-sidebar -->

        <div id="js-repo-pjax-container" class="repository-content context-loader-container" data-pjax-container>
          


<!-- blob contrib key: blob_contributors:v21:468aaf205e3cbb7eab41e504d7e67936 -->

<p title="This is a placeholder element" class="js-history-link-replace hidden"></p>

<a href="/balupton/jquery-scrollto/find/master" data-pjax data-hotkey="t" class="js-show-file-finder" style="display:none">Show File Finder</a>

<div class="file-navigation">
  

<div class="select-menu js-menu-container js-select-menu" >
  <span class="minibutton select-menu-button js-menu-target" data-hotkey="w"
    data-master-branch="master"
    data-ref="master"
    role="button" aria-label="Switch branches or tags" tabindex="0" aria-haspopup="true">
    <span class="octicon octicon-git-branch"></span>
    <i>branch:</i>
    <span class="js-select-button">master</span>
  </span>

  <div class="select-menu-modal-holder js-menu-content js-navigation-container" data-pjax aria-hidden="true">

    <div class="select-menu-modal">
      <div class="select-menu-header">
        <span class="select-menu-title">Switch branches/tags</span>
        <span class="octicon octicon-remove-close js-menu-close"></span>
      </div> <!-- /.select-menu-header -->

      <div class="select-menu-filters">
        <div class="select-menu-text-filter">
          <input type="text" aria-label="Filter branches/tags" id="context-commitish-filter-field" class="js-filterable-field js-navigation-enable" placeholder="Filter branches/tags">
        </div>
        <div class="select-menu-tabs">
          <ul>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="branches" class="js-select-menu-tab">Branches</a>
            </li>
            <li class="select-menu-tab">
              <a href="#" data-tab-filter="tags" class="js-select-menu-tab">Tags</a>
            </li>
          </ul>
        </div><!-- /.select-menu-tabs -->
      </div><!-- /.select-menu-filters -->

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="branches">

        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/blob/dev/src/documents/lib/jquery-scrollto.js"
                 data-name="dev"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="dev">dev</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/blob/gh-pages/src/documents/lib/jquery-scrollto.js"
                 data-name="gh-pages"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="gh-pages">gh-pages</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item selected">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/blob/master/src/documents/lib/jquery-scrollto.js"
                 data-name="master"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="master">master</a>
            </div> <!-- /.select-menu-item -->
        </div>

          <div class="select-menu-no-results">Nothing to show</div>
      </div> <!-- /.select-menu-list -->

      <div class="select-menu-list select-menu-tab-bucket js-select-menu-tab-bucket" data-tab-filter="tags">
        <div data-filterable-for="context-commitish-filter-field" data-filterable-type="substring">


            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.4.4/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.4.4"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.4.4">v1.4.4</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.4.3/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.4.3"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.4.3">v1.4.3</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.4.2/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.4.2"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.4.2">v1.4.2</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.4.1/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.4.1"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.4.1">v1.4.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.3.0/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.3.0"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.3.0">v1.3.0</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.2.1/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.2.1"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.2.1">v1.2.1</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.0.1-beta/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.0.1-beta"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.0.1-beta">v1.0.1-beta</a>
            </div> <!-- /.select-menu-item -->
            <div class="select-menu-item js-navigation-item ">
              <span class="select-menu-item-icon octicon octicon-check"></span>
              <a href="/balupton/jquery-scrollto/tree/v1.0.0-beta/src/documents/lib/jquery-scrollto.js"
                 data-name="v1.0.0-beta"
                 data-skip-pjax="true"
                 rel="nofollow"
                 class="js-navigation-open select-menu-item-text js-select-button-text css-truncate-target"
                 title="v1.0.0-beta">v1.0.0-beta</a>
            </div> <!-- /.select-menu-item -->
        </div>

        <div class="select-menu-no-results">Nothing to show</div>
      </div> <!-- /.select-menu-list -->

    </div> <!-- /.select-menu-modal -->
  </div> <!-- /.select-menu-modal-holder -->
</div> <!-- /.select-menu -->

  <div class="breadcrumb">
    <span class='repo-root js-repo-root'><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/balupton/jquery-scrollto" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">jquery-scrollto</span></a></span></span><span class="separator"> / </span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/balupton/jquery-scrollto/tree/master/src" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">src</span></a></span><span class="separator"> / </span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/balupton/jquery-scrollto/tree/master/src/documents" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">documents</span></a></span><span class="separator"> / </span><span itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb"><a href="/balupton/jquery-scrollto/tree/master/src/documents/lib" data-branch="master" data-direction="back" data-pjax="true" itemscope="url"><span itemprop="title">lib</span></a></span><span class="separator"> / </span><strong class="final-path">jquery-scrollto.js</strong> <span aria-label="copy to clipboard" class="js-zeroclipboard minibutton zeroclipboard-button" data-clipboard-text="src/documents/lib/jquery-scrollto.js" data-copied-hint="copied!"><span class="octicon octicon-clippy"></span></span>
  </div>
</div>


  <div class="commit file-history-tease">
    <img alt="Benjamin Arthur Lupton" class="main-avatar js-avatar" data-user="61148" height="24" src="https://avatars2.githubusercontent.com/u/61148?s=140" width="24" />
    <span class="author"><a href="/balupton" rel="author">balupton</a></span>
    <time class="js-relative-date" data-title-format="YYYY-MM-DD HH:mm:ss" datetime="2013-05-29T21:08:17+08:00" title="2013-05-29 09:08:17">May 29, 2013</time>
    <div class="commit-title">
        <a href="/balupton/jquery-scrollto/commit/8aafe26219262907ee72b6e3a785fd7f791bb55d" class="message" data-pjax="true" title="Now uses the docpad umd plugin">Now uses the docpad umd plugin</a>
    </div>

    <div class="participation">
      <p class="quickstat"><a href="#blob_contributors_box" rel="facebox"><strong>1</strong> contributor</a></p>
      
    </div>
    <div id="blob_contributors_box" style="display:none">
      <h2 class="facebox-header">Users who have contributed to this file</h2>
      <ul class="facebox-user-list">
          <li class="facebox-user-list-item">
            <img alt="Benjamin Arthur Lupton" class=" js-avatar" data-user="61148" height="24" src="https://avatars2.githubusercontent.com/u/61148?s=140" width="24" />
            <a href="/balupton">balupton</a>
          </li>
      </ul>
    </div>
  </div>

<div class="file-box">
  <div class="file">
    <div class="meta clearfix">
      <div class="info file-name">
        <span class="icon"><b class="octicon octicon-file-text"></b></span>
        <span class="mode" title="File Mode">file</span>
        <span class="meta-divider"></span>
          <span>260 lines (226 sloc)</span>
          <span class="meta-divider"></span>
        <span>7.442 kb</span>
      </div>
      <div class="actions">
        <div class="button-group">
            <a class="minibutton tooltipped tooltipped-w"
               href="http://windows.github.com" aria-label="Open this file in GitHub for Windows">
                <span class="octicon octicon-device-desktop"></span> Open
            </a>
                <a class="minibutton tooltipped tooltipped-n js-update-url-with-hash"
                   aria-label="Clicking this button will automatically fork this project so you can edit the file"
                   href="/balupton/jquery-scrollto/edit/master/src/documents/lib/jquery-scrollto.js"
                   data-method="post" rel="nofollow">Edit</a>
          <a href="/balupton/jquery-scrollto/raw/master/src/documents/lib/jquery-scrollto.js" class="button minibutton " id="raw-url">Raw</a>
            <a href="/balupton/jquery-scrollto/blame/master/src/documents/lib/jquery-scrollto.js" class="button minibutton js-update-url-with-hash">Blame</a>
          <a href="/balupton/jquery-scrollto/commits/master/src/documents/lib/jquery-scrollto.js" class="button minibutton " rel="nofollow">History</a>
        </div><!-- /.button-group -->

            <a class="minibutton danger empty-icon tooltipped tooltipped-s"
               href="/balupton/jquery-scrollto/delete/master/src/documents/lib/jquery-scrollto.js"
               aria-label="Fork this project and delete file"
               data-method="post" data-test-id="delete-blob-file" rel="nofollow">

          Delete
        </a>
      </div><!-- /.actions -->
    </div>
        <div class="blob-wrapper data type-javascript js-blob-data">
        <table class="file-code file-diff tab-size-8">
          <tr class="file-code-line">
            <td class="blob-line-nums">
              <span id="L1" rel="#L1">1</span>
<span id="L2" rel="#L2">2</span>
<span id="L3" rel="#L3">3</span>
<span id="L4" rel="#L4">4</span>
<span id="L5" rel="#L5">5</span>
<span id="L6" rel="#L6">6</span>
<span id="L7" rel="#L7">7</span>
<span id="L8" rel="#L8">8</span>
<span id="L9" rel="#L9">9</span>
<span id="L10" rel="#L10">10</span>
<span id="L11" rel="#L11">11</span>
<span id="L12" rel="#L12">12</span>
<span id="L13" rel="#L13">13</span>
<span id="L14" rel="#L14">14</span>
<span id="L15" rel="#L15">15</span>
<span id="L16" rel="#L16">16</span>
<span id="L17" rel="#L17">17</span>
<span id="L18" rel="#L18">18</span>
<span id="L19" rel="#L19">19</span>
<span id="L20" rel="#L20">20</span>
<span id="L21" rel="#L21">21</span>
<span id="L22" rel="#L22">22</span>
<span id="L23" rel="#L23">23</span>
<span id="L24" rel="#L24">24</span>
<span id="L25" rel="#L25">25</span>
<span id="L26" rel="#L26">26</span>
<span id="L27" rel="#L27">27</span>
<span id="L28" rel="#L28">28</span>
<span id="L29" rel="#L29">29</span>
<span id="L30" rel="#L30">30</span>
<span id="L31" rel="#L31">31</span>
<span id="L32" rel="#L32">32</span>
<span id="L33" rel="#L33">33</span>
<span id="L34" rel="#L34">34</span>
<span id="L35" rel="#L35">35</span>
<span id="L36" rel="#L36">36</span>
<span id="L37" rel="#L37">37</span>
<span id="L38" rel="#L38">38</span>
<span id="L39" rel="#L39">39</span>
<span id="L40" rel="#L40">40</span>
<span id="L41" rel="#L41">41</span>
<span id="L42" rel="#L42">42</span>
<span id="L43" rel="#L43">43</span>
<span id="L44" rel="#L44">44</span>
<span id="L45" rel="#L45">45</span>
<span id="L46" rel="#L46">46</span>
<span id="L47" rel="#L47">47</span>
<span id="L48" rel="#L48">48</span>
<span id="L49" rel="#L49">49</span>
<span id="L50" rel="#L50">50</span>
<span id="L51" rel="#L51">51</span>
<span id="L52" rel="#L52">52</span>
<span id="L53" rel="#L53">53</span>
<span id="L54" rel="#L54">54</span>
<span id="L55" rel="#L55">55</span>
<span id="L56" rel="#L56">56</span>
<span id="L57" rel="#L57">57</span>
<span id="L58" rel="#L58">58</span>
<span id="L59" rel="#L59">59</span>
<span id="L60" rel="#L60">60</span>
<span id="L61" rel="#L61">61</span>
<span id="L62" rel="#L62">62</span>
<span id="L63" rel="#L63">63</span>
<span id="L64" rel="#L64">64</span>
<span id="L65" rel="#L65">65</span>
<span id="L66" rel="#L66">66</span>
<span id="L67" rel="#L67">67</span>
<span id="L68" rel="#L68">68</span>
<span id="L69" rel="#L69">69</span>
<span id="L70" rel="#L70">70</span>
<span id="L71" rel="#L71">71</span>
<span id="L72" rel="#L72">72</span>
<span id="L73" rel="#L73">73</span>
<span id="L74" rel="#L74">74</span>
<span id="L75" rel="#L75">75</span>
<span id="L76" rel="#L76">76</span>
<span id="L77" rel="#L77">77</span>
<span id="L78" rel="#L78">78</span>
<span id="L79" rel="#L79">79</span>
<span id="L80" rel="#L80">80</span>
<span id="L81" rel="#L81">81</span>
<span id="L82" rel="#L82">82</span>
<span id="L83" rel="#L83">83</span>
<span id="L84" rel="#L84">84</span>
<span id="L85" rel="#L85">85</span>
<span id="L86" rel="#L86">86</span>
<span id="L87" rel="#L87">87</span>
<span id="L88" rel="#L88">88</span>
<span id="L89" rel="#L89">89</span>
<span id="L90" rel="#L90">90</span>
<span id="L91" rel="#L91">91</span>
<span id="L92" rel="#L92">92</span>
<span id="L93" rel="#L93">93</span>
<span id="L94" rel="#L94">94</span>
<span id="L95" rel="#L95">95</span>
<span id="L96" rel="#L96">96</span>
<span id="L97" rel="#L97">97</span>
<span id="L98" rel="#L98">98</span>
<span id="L99" rel="#L99">99</span>
<span id="L100" rel="#L100">100</span>
<span id="L101" rel="#L101">101</span>
<span id="L102" rel="#L102">102</span>
<span id="L103" rel="#L103">103</span>
<span id="L104" rel="#L104">104</span>
<span id="L105" rel="#L105">105</span>
<span id="L106" rel="#L106">106</span>
<span id="L107" rel="#L107">107</span>
<span id="L108" rel="#L108">108</span>
<span id="L109" rel="#L109">109</span>
<span id="L110" rel="#L110">110</span>
<span id="L111" rel="#L111">111</span>
<span id="L112" rel="#L112">112</span>
<span id="L113" rel="#L113">113</span>
<span id="L114" rel="#L114">114</span>
<span id="L115" rel="#L115">115</span>
<span id="L116" rel="#L116">116</span>
<span id="L117" rel="#L117">117</span>
<span id="L118" rel="#L118">118</span>
<span id="L119" rel="#L119">119</span>
<span id="L120" rel="#L120">120</span>
<span id="L121" rel="#L121">121</span>
<span id="L122" rel="#L122">122</span>
<span id="L123" rel="#L123">123</span>
<span id="L124" rel="#L124">124</span>
<span id="L125" rel="#L125">125</span>
<span id="L126" rel="#L126">126</span>
<span id="L127" rel="#L127">127</span>
<span id="L128" rel="#L128">128</span>
<span id="L129" rel="#L129">129</span>
<span id="L130" rel="#L130">130</span>
<span id="L131" rel="#L131">131</span>
<span id="L132" rel="#L132">132</span>
<span id="L133" rel="#L133">133</span>
<span id="L134" rel="#L134">134</span>
<span id="L135" rel="#L135">135</span>
<span id="L136" rel="#L136">136</span>
<span id="L137" rel="#L137">137</span>
<span id="L138" rel="#L138">138</span>
<span id="L139" rel="#L139">139</span>
<span id="L140" rel="#L140">140</span>
<span id="L141" rel="#L141">141</span>
<span id="L142" rel="#L142">142</span>
<span id="L143" rel="#L143">143</span>
<span id="L144" rel="#L144">144</span>
<span id="L145" rel="#L145">145</span>
<span id="L146" rel="#L146">146</span>
<span id="L147" rel="#L147">147</span>
<span id="L148" rel="#L148">148</span>
<span id="L149" rel="#L149">149</span>
<span id="L150" rel="#L150">150</span>
<span id="L151" rel="#L151">151</span>
<span id="L152" rel="#L152">152</span>
<span id="L153" rel="#L153">153</span>
<span id="L154" rel="#L154">154</span>
<span id="L155" rel="#L155">155</span>
<span id="L156" rel="#L156">156</span>
<span id="L157" rel="#L157">157</span>
<span id="L158" rel="#L158">158</span>
<span id="L159" rel="#L159">159</span>
<span id="L160" rel="#L160">160</span>
<span id="L161" rel="#L161">161</span>
<span id="L162" rel="#L162">162</span>
<span id="L163" rel="#L163">163</span>
<span id="L164" rel="#L164">164</span>
<span id="L165" rel="#L165">165</span>
<span id="L166" rel="#L166">166</span>
<span id="L167" rel="#L167">167</span>
<span id="L168" rel="#L168">168</span>
<span id="L169" rel="#L169">169</span>
<span id="L170" rel="#L170">170</span>
<span id="L171" rel="#L171">171</span>
<span id="L172" rel="#L172">172</span>
<span id="L173" rel="#L173">173</span>
<span id="L174" rel="#L174">174</span>
<span id="L175" rel="#L175">175</span>
<span id="L176" rel="#L176">176</span>
<span id="L177" rel="#L177">177</span>
<span id="L178" rel="#L178">178</span>
<span id="L179" rel="#L179">179</span>
<span id="L180" rel="#L180">180</span>
<span id="L181" rel="#L181">181</span>
<span id="L182" rel="#L182">182</span>
<span id="L183" rel="#L183">183</span>
<span id="L184" rel="#L184">184</span>
<span id="L185" rel="#L185">185</span>
<span id="L186" rel="#L186">186</span>
<span id="L187" rel="#L187">187</span>
<span id="L188" rel="#L188">188</span>
<span id="L189" rel="#L189">189</span>
<span id="L190" rel="#L190">190</span>
<span id="L191" rel="#L191">191</span>
<span id="L192" rel="#L192">192</span>
<span id="L193" rel="#L193">193</span>
<span id="L194" rel="#L194">194</span>
<span id="L195" rel="#L195">195</span>
<span id="L196" rel="#L196">196</span>
<span id="L197" rel="#L197">197</span>
<span id="L198" rel="#L198">198</span>
<span id="L199" rel="#L199">199</span>
<span id="L200" rel="#L200">200</span>
<span id="L201" rel="#L201">201</span>
<span id="L202" rel="#L202">202</span>
<span id="L203" rel="#L203">203</span>
<span id="L204" rel="#L204">204</span>
<span id="L205" rel="#L205">205</span>
<span id="L206" rel="#L206">206</span>
<span id="L207" rel="#L207">207</span>
<span id="L208" rel="#L208">208</span>
<span id="L209" rel="#L209">209</span>
<span id="L210" rel="#L210">210</span>
<span id="L211" rel="#L211">211</span>
<span id="L212" rel="#L212">212</span>
<span id="L213" rel="#L213">213</span>
<span id="L214" rel="#L214">214</span>
<span id="L215" rel="#L215">215</span>
<span id="L216" rel="#L216">216</span>
<span id="L217" rel="#L217">217</span>
<span id="L218" rel="#L218">218</span>
<span id="L219" rel="#L219">219</span>
<span id="L220" rel="#L220">220</span>
<span id="L221" rel="#L221">221</span>
<span id="L222" rel="#L222">222</span>
<span id="L223" rel="#L223">223</span>
<span id="L224" rel="#L224">224</span>
<span id="L225" rel="#L225">225</span>
<span id="L226" rel="#L226">226</span>
<span id="L227" rel="#L227">227</span>
<span id="L228" rel="#L228">228</span>
<span id="L229" rel="#L229">229</span>
<span id="L230" rel="#L230">230</span>
<span id="L231" rel="#L231">231</span>
<span id="L232" rel="#L232">232</span>
<span id="L233" rel="#L233">233</span>
<span id="L234" rel="#L234">234</span>
<span id="L235" rel="#L235">235</span>
<span id="L236" rel="#L236">236</span>
<span id="L237" rel="#L237">237</span>
<span id="L238" rel="#L238">238</span>
<span id="L239" rel="#L239">239</span>
<span id="L240" rel="#L240">240</span>
<span id="L241" rel="#L241">241</span>
<span id="L242" rel="#L242">242</span>
<span id="L243" rel="#L243">243</span>
<span id="L244" rel="#L244">244</span>
<span id="L245" rel="#L245">245</span>
<span id="L246" rel="#L246">246</span>
<span id="L247" rel="#L247">247</span>
<span id="L248" rel="#L248">248</span>
<span id="L249" rel="#L249">249</span>
<span id="L250" rel="#L250">250</span>
<span id="L251" rel="#L251">251</span>
<span id="L252" rel="#L252">252</span>
<span id="L253" rel="#L253">253</span>
<span id="L254" rel="#L254">254</span>
<span id="L255" rel="#L255">255</span>
<span id="L256" rel="#L256">256</span>
<span id="L257" rel="#L257">257</span>
<span id="L258" rel="#L258">258</span>
<span id="L259" rel="#L259">259</span>
<span id="L260" rel="#L260">260</span>

            </td>
            <td class="blob-line-code"><div class="code-body highlight"><pre><div class='line' id='LC1'><span class="o">---</span></div><div class='line' id='LC2'><span class="nx">umd</span><span class="o">:</span> <span class="kc">true</span></div><div class='line' id='LC3'><span class="o">---</span></div><div class='line' id='LC4'><br/></div><div class='line' id='LC5'><span class="p">(</span><span class="kd">function</span><span class="p">(){</span></div><div class='line' id='LC6'>	<span class="c1">// Prepare</span></div><div class='line' id='LC7'>	<span class="kd">var</span> <span class="nx">jQuery</span><span class="p">,</span> <span class="nx">$</span><span class="p">,</span> <span class="nx">ScrollTo</span><span class="p">;</span></div><div class='line' id='LC8'>	<span class="nx">jQuery</span> <span class="o">=</span> <span class="nx">$</span> <span class="o">=</span> <span class="nb">window</span><span class="p">.</span><span class="nx">jQuery</span> <span class="o">||</span> <span class="nx">require</span><span class="p">(</span><span class="s1">&#39;jquery&#39;</span><span class="p">);</span></div><div class='line' id='LC9'><br/></div><div class='line' id='LC10'>	<span class="c1">// Fix scrolling animations on html/body on safari</span></div><div class='line' id='LC11'>	<span class="nx">$</span><span class="p">.</span><span class="nx">propHooks</span><span class="p">.</span><span class="nx">scrollTop</span> <span class="o">=</span> <span class="nx">$</span><span class="p">.</span><span class="nx">propHooks</span><span class="p">.</span><span class="nx">scrollLeft</span> <span class="o">=</span> <span class="p">{</span></div><div class='line' id='LC12'>		<span class="nx">get</span><span class="o">:</span> <span class="kd">function</span><span class="p">(</span><span class="nx">elem</span><span class="p">,</span><span class="nx">prop</span><span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC13'>			<span class="kd">var</span> <span class="nx">result</span> <span class="o">=</span> <span class="kc">null</span><span class="p">;</span></div><div class='line' id='LC14'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">elem</span><span class="p">.</span><span class="nx">tagName</span> <span class="o">===</span> <span class="s1">&#39;HTML&#39;</span> <span class="o">||</span> <span class="nx">elem</span><span class="p">.</span><span class="nx">tagName</span> <span class="o">===</span> <span class="s1">&#39;BODY&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC15'>				<span class="k">if</span> <span class="p">(</span> <span class="nx">prop</span> <span class="o">===</span> <span class="s1">&#39;scrollLeft&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC16'>					<span class="nx">result</span> <span class="o">=</span> <span class="nb">window</span><span class="p">.</span><span class="nx">scrollX</span><span class="p">;</span></div><div class='line' id='LC17'>				<span class="p">}</span> <span class="k">else</span> <span class="k">if</span> <span class="p">(</span> <span class="nx">prop</span> <span class="o">===</span> <span class="s1">&#39;scrollTop&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC18'>					<span class="nx">result</span> <span class="o">=</span> <span class="nb">window</span><span class="p">.</span><span class="nx">scrollY</span><span class="p">;</span></div><div class='line' id='LC19'>				<span class="p">}</span></div><div class='line' id='LC20'>			<span class="p">}</span></div><div class='line' id='LC21'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">result</span> <span class="o">==</span> <span class="kc">null</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC22'>				<span class="nx">result</span> <span class="o">=</span> <span class="nx">elem</span><span class="p">[</span><span class="nx">prop</span><span class="p">];</span></div><div class='line' id='LC23'>			<span class="p">}</span></div><div class='line' id='LC24'>			<span class="k">return</span> <span class="nx">result</span><span class="p">;</span></div><div class='line' id='LC25'>		<span class="p">}</span></div><div class='line' id='LC26'>	<span class="p">};</span></div><div class='line' id='LC27'>	<span class="nx">$</span><span class="p">.</span><span class="nx">Tween</span><span class="p">.</span><span class="nx">propHooks</span><span class="p">.</span><span class="nx">scrollTop</span> <span class="o">=</span> <span class="nx">$</span><span class="p">.</span><span class="nx">Tween</span><span class="p">.</span><span class="nx">propHooks</span><span class="p">.</span><span class="nx">scrollLeft</span> <span class="o">=</span> <span class="p">{</span></div><div class='line' id='LC28'>		<span class="nx">get</span><span class="o">:</span> <span class="kd">function</span><span class="p">(</span><span class="nx">tween</span><span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC29'>			<span class="k">return</span> <span class="nx">$</span><span class="p">.</span><span class="nx">propHooks</span><span class="p">.</span><span class="nx">scrollTop</span><span class="p">.</span><span class="nx">get</span><span class="p">(</span><span class="nx">tween</span><span class="p">.</span><span class="nx">elem</span><span class="p">,</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">prop</span><span class="p">);</span></div><div class='line' id='LC30'>		<span class="p">},</span></div><div class='line' id='LC31'>		<span class="nx">set</span><span class="o">:</span> <span class="kd">function</span><span class="p">(</span><span class="nx">tween</span><span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC32'>			<span class="c1">// Our safari fix</span></div><div class='line' id='LC33'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">elem</span><span class="p">.</span><span class="nx">tagName</span> <span class="o">===</span> <span class="s1">&#39;HTML&#39;</span> <span class="o">||</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">elem</span><span class="p">.</span><span class="nx">tagName</span> <span class="o">===</span> <span class="s1">&#39;BODY&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC34'>				<span class="c1">// Defaults</span></div><div class='line' id='LC35'>				<span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollLeft</span> <span class="o">=</span> <span class="p">(</span><span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollLeft</span> <span class="o">||</span> <span class="nb">window</span><span class="p">.</span><span class="nx">scrollX</span><span class="p">);</span></div><div class='line' id='LC36'>				<span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollTop</span> <span class="o">=</span> <span class="p">(</span><span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollTop</span> <span class="o">||</span> <span class="nb">window</span><span class="p">.</span><span class="nx">scrollY</span><span class="p">);</span></div><div class='line' id='LC37'><br/></div><div class='line' id='LC38'>				<span class="c1">// Apply</span></div><div class='line' id='LC39'>				<span class="k">if</span> <span class="p">(</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">prop</span> <span class="o">===</span> <span class="s1">&#39;scrollLeft&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC40'>					<span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollLeft</span> <span class="o">=</span> <span class="nb">Math</span><span class="p">.</span><span class="nx">round</span><span class="p">(</span><span class="nx">tween</span><span class="p">.</span><span class="nx">now</span><span class="p">);</span></div><div class='line' id='LC41'>				<span class="p">}</span></div><div class='line' id='LC42'>				<span class="k">else</span> <span class="k">if</span> <span class="p">(</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">prop</span> <span class="o">===</span> <span class="s1">&#39;scrollTop&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC43'>					<span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollTop</span> <span class="o">=</span> <span class="nb">Math</span><span class="p">.</span><span class="nx">round</span><span class="p">(</span><span class="nx">tween</span><span class="p">.</span><span class="nx">now</span><span class="p">);</span></div><div class='line' id='LC44'>				<span class="p">}</span></div><div class='line' id='LC45'><br/></div><div class='line' id='LC46'>				<span class="c1">// Apply</span></div><div class='line' id='LC47'>				<span class="nb">window</span><span class="p">.</span><span class="nx">scrollTo</span><span class="p">(</span><span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollLeft</span><span class="p">,</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">options</span><span class="p">.</span><span class="nx">bodyScrollTop</span><span class="p">);</span></div><div class='line' id='LC48'>			<span class="p">}</span></div><div class='line' id='LC49'>			<span class="c1">// jQuery&#39;s IE8 Fix</span></div><div class='line' id='LC50'>			<span class="k">else</span> <span class="k">if</span> <span class="p">(</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">elem</span><span class="p">.</span><span class="nx">nodeType</span> <span class="o">&amp;&amp;</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">elem</span><span class="p">.</span><span class="nx">parentNode</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC51'>				<span class="nx">tween</span><span class="p">.</span><span class="nx">elem</span><span class="p">[</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">prop</span> <span class="p">]</span> <span class="o">=</span> <span class="nx">tween</span><span class="p">.</span><span class="nx">now</span><span class="p">;</span></div><div class='line' id='LC52'>			<span class="p">}</span></div><div class='line' id='LC53'>		<span class="p">}</span></div><div class='line' id='LC54'>	<span class="p">};</span></div><div class='line' id='LC55'><br/></div><div class='line' id='LC56'>	<span class="c1">// jQuery ScrollTo</span></div><div class='line' id='LC57'>	<span class="nx">ScrollTo</span> <span class="o">=</span> <span class="p">{</span></div><div class='line' id='LC58'>		<span class="c1">// Configuration</span></div><div class='line' id='LC59'>		<span class="nx">config</span><span class="o">:</span> <span class="p">{</span></div><div class='line' id='LC60'>			<span class="nx">duration</span><span class="o">:</span> <span class="mi">400</span><span class="p">,</span></div><div class='line' id='LC61'>			<span class="nx">easing</span><span class="o">:</span> <span class="s1">&#39;swing&#39;</span><span class="p">,</span></div><div class='line' id='LC62'>			<span class="nx">callback</span><span class="o">:</span> <span class="kc">undefined</span><span class="p">,</span></div><div class='line' id='LC63'>			<span class="nx">durationMode</span><span class="o">:</span> <span class="s1">&#39;each&#39;</span><span class="p">,</span></div><div class='line' id='LC64'>			<span class="nx">offsetTop</span><span class="o">:</span> <span class="mi">0</span><span class="p">,</span></div><div class='line' id='LC65'>			<span class="nx">offsetLeft</span><span class="o">:</span> <span class="mi">0</span></div><div class='line' id='LC66'>		<span class="p">},</span></div><div class='line' id='LC67'><br/></div><div class='line' id='LC68'>		<span class="c1">// Set Configuration</span></div><div class='line' id='LC69'>		<span class="nx">configure</span><span class="o">:</span> <span class="kd">function</span><span class="p">(</span><span class="nx">options</span><span class="p">){</span></div><div class='line' id='LC70'>			<span class="c1">// Apply Options to Config</span></div><div class='line' id='LC71'>			<span class="nx">$</span><span class="p">.</span><span class="nx">extend</span><span class="p">(</span><span class="nx">ScrollTo</span><span class="p">.</span><span class="nx">config</span><span class="p">,</span> <span class="nx">options</span><span class="o">||</span><span class="p">{});</span></div><div class='line' id='LC72'><br/></div><div class='line' id='LC73'>			<span class="c1">// Chain</span></div><div class='line' id='LC74'>			<span class="k">return</span> <span class="k">this</span><span class="p">;</span></div><div class='line' id='LC75'>		<span class="p">},</span></div><div class='line' id='LC76'><br/></div><div class='line' id='LC77'>		<span class="c1">// Perform the Scroll Animation for the Collections</span></div><div class='line' id='LC78'>		<span class="c1">// We use $inline here, so we can determine the actual offset start for each overflow:scroll item</span></div><div class='line' id='LC79'>		<span class="c1">// Each collection is for each overflow:scroll item</span></div><div class='line' id='LC80'>		<span class="nx">scroll</span><span class="o">:</span> <span class="kd">function</span><span class="p">(</span><span class="nx">collections</span><span class="p">,</span> <span class="nx">config</span><span class="p">){</span></div><div class='line' id='LC81'>			<span class="c1">// Prepare</span></div><div class='line' id='LC82'>			<span class="kd">var</span> <span class="nx">collection</span><span class="p">,</span> <span class="nx">$container</span><span class="p">,</span> <span class="nx">container</span><span class="p">,</span> <span class="nx">$target</span><span class="p">,</span> <span class="nx">$inline</span><span class="p">,</span> <span class="nx">position</span><span class="p">,</span> <span class="nx">containerTagName</span><span class="p">,</span></div><div class='line' id='LC83'>				<span class="nx">containerScrollTop</span><span class="p">,</span> <span class="nx">containerScrollLeft</span><span class="p">,</span></div><div class='line' id='LC84'>				<span class="nx">containerScrollTopEnd</span><span class="p">,</span> <span class="nx">containerScrollLeftEnd</span><span class="p">,</span></div><div class='line' id='LC85'>				<span class="nx">startOffsetTop</span><span class="p">,</span> <span class="nx">targetOffsetTop</span><span class="p">,</span> <span class="nx">targetOffsetTopAdjusted</span><span class="p">,</span></div><div class='line' id='LC86'>				<span class="nx">startOffsetLeft</span><span class="p">,</span> <span class="nx">targetOffsetLeft</span><span class="p">,</span> <span class="nx">targetOffsetLeftAdjusted</span><span class="p">,</span></div><div class='line' id='LC87'>				<span class="nx">scrollOptions</span><span class="p">,</span></div><div class='line' id='LC88'>				<span class="nx">callback</span><span class="p">;</span></div><div class='line' id='LC89'><br/></div><div class='line' id='LC90'>			<span class="c1">// Determine the Scroll</span></div><div class='line' id='LC91'>			<span class="nx">collection</span> <span class="o">=</span> <span class="nx">collections</span><span class="p">.</span><span class="nx">pop</span><span class="p">();</span></div><div class='line' id='LC92'>			<span class="nx">$container</span> <span class="o">=</span> <span class="nx">collection</span><span class="p">.</span><span class="nx">$container</span><span class="p">;</span></div><div class='line' id='LC93'>			<span class="nx">$target</span> <span class="o">=</span> <span class="nx">collection</span><span class="p">.</span><span class="nx">$target</span><span class="p">;</span></div><div class='line' id='LC94'>			<span class="nx">containerTagName</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">prop</span><span class="p">(</span><span class="s1">&#39;tagName&#39;</span><span class="p">);</span></div><div class='line' id='LC95'><br/></div><div class='line' id='LC96'>			<span class="c1">// Prepare the Inline Element of the Container</span></div><div class='line' id='LC97'>			<span class="nx">$inline</span> <span class="o">=</span> <span class="nx">$</span><span class="p">(</span><span class="s1">&#39;&lt;span/&gt;&#39;</span><span class="p">).</span><span class="nx">css</span><span class="p">({</span></div><div class='line' id='LC98'>				<span class="s1">&#39;position&#39;</span><span class="o">:</span> <span class="s1">&#39;absolute&#39;</span><span class="p">,</span></div><div class='line' id='LC99'>				<span class="s1">&#39;top&#39;</span><span class="o">:</span> <span class="s1">&#39;0px&#39;</span><span class="p">,</span></div><div class='line' id='LC100'>				<span class="s1">&#39;left&#39;</span><span class="o">:</span> <span class="s1">&#39;0px&#39;</span></div><div class='line' id='LC101'>			<span class="p">});</span></div><div class='line' id='LC102'>			<span class="nx">position</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">css</span><span class="p">(</span><span class="s1">&#39;position&#39;</span><span class="p">);</span></div><div class='line' id='LC103'><br/></div><div class='line' id='LC104'>			<span class="c1">// Insert the Inline Element of the Container</span></div><div class='line' id='LC105'>			<span class="nx">$container</span><span class="p">.</span><span class="nx">css</span><span class="p">({</span><span class="nx">position</span><span class="o">:</span><span class="s1">&#39;relative&#39;</span><span class="p">});</span></div><div class='line' id='LC106'>			<span class="nx">$inline</span><span class="p">.</span><span class="nx">appendTo</span><span class="p">(</span><span class="nx">$container</span><span class="p">);</span></div><div class='line' id='LC107'><br/></div><div class='line' id='LC108'>			<span class="c1">// Determine the top offset</span></div><div class='line' id='LC109'>			<span class="nx">startOffsetTop</span> <span class="o">=</span> <span class="nx">$inline</span><span class="p">.</span><span class="nx">offset</span><span class="p">().</span><span class="nx">top</span><span class="p">;</span></div><div class='line' id='LC110'>			<span class="nx">targetOffsetTop</span> <span class="o">=</span> <span class="nx">$target</span><span class="p">.</span><span class="nx">offset</span><span class="p">().</span><span class="nx">top</span><span class="p">;</span></div><div class='line' id='LC111'>			<span class="nx">targetOffsetTopAdjusted</span> <span class="o">=</span> <span class="nx">targetOffsetTop</span> <span class="o">-</span> <span class="nx">startOffsetTop</span> <span class="o">-</span> <span class="nb">parseInt</span><span class="p">(</span><span class="nx">config</span><span class="p">.</span><span class="nx">offsetTop</span><span class="p">,</span><span class="mi">10</span><span class="p">);</span></div><div class='line' id='LC112'><br/></div><div class='line' id='LC113'>			<span class="c1">// Determine the left offset</span></div><div class='line' id='LC114'>			<span class="nx">startOffsetLeft</span> <span class="o">=</span> <span class="nx">$inline</span><span class="p">.</span><span class="nx">offset</span><span class="p">().</span><span class="nx">left</span><span class="p">;</span></div><div class='line' id='LC115'>			<span class="nx">targetOffsetLeft</span> <span class="o">=</span> <span class="nx">$target</span><span class="p">.</span><span class="nx">offset</span><span class="p">().</span><span class="nx">left</span><span class="p">;</span></div><div class='line' id='LC116'>			<span class="nx">targetOffsetLeftAdjusted</span> <span class="o">=</span> <span class="nx">targetOffsetLeft</span> <span class="o">-</span> <span class="nx">startOffsetLeft</span> <span class="o">-</span> <span class="nb">parseInt</span><span class="p">(</span><span class="nx">config</span><span class="p">.</span><span class="nx">offsetLeft</span><span class="p">,</span><span class="mi">10</span><span class="p">);</span></div><div class='line' id='LC117'><br/></div><div class='line' id='LC118'>			<span class="c1">// Determine current scroll positions</span></div><div class='line' id='LC119'>			<span class="nx">containerScrollTop</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">prop</span><span class="p">(</span><span class="s1">&#39;scrollTop&#39;</span><span class="p">);</span></div><div class='line' id='LC120'>			<span class="nx">containerScrollLeft</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">prop</span><span class="p">(</span><span class="s1">&#39;scrollLeft&#39;</span><span class="p">);</span></div><div class='line' id='LC121'><br/></div><div class='line' id='LC122'>			<span class="c1">// Reset the Inline Element of the Container</span></div><div class='line' id='LC123'>			<span class="nx">$inline</span><span class="p">.</span><span class="nx">remove</span><span class="p">();</span></div><div class='line' id='LC124'>			<span class="nx">$container</span><span class="p">.</span><span class="nx">css</span><span class="p">({</span><span class="nx">position</span><span class="o">:</span><span class="nx">position</span><span class="p">});</span></div><div class='line' id='LC125'><br/></div><div class='line' id='LC126'>			<span class="c1">// Prepare the scroll options</span></div><div class='line' id='LC127'>			<span class="nx">scrollOptions</span> <span class="o">=</span> <span class="p">{};</span></div><div class='line' id='LC128'><br/></div><div class='line' id='LC129'>			<span class="c1">// Prepare the callback</span></div><div class='line' id='LC130'>			<span class="nx">callback</span> <span class="o">=</span> <span class="kd">function</span><span class="p">(</span><span class="nx">event</span><span class="p">){</span></div><div class='line' id='LC131'>				<span class="c1">// Check</span></div><div class='line' id='LC132'>				<span class="k">if</span> <span class="p">(</span> <span class="nx">collections</span><span class="p">.</span><span class="nx">length</span> <span class="o">===</span> <span class="mi">0</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC133'>					<span class="c1">// Callback</span></div><div class='line' id='LC134'>					<span class="k">if</span> <span class="p">(</span> <span class="k">typeof</span> <span class="nx">config</span><span class="p">.</span><span class="nx">callback</span> <span class="o">===</span> <span class="s1">&#39;function&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC135'>						<span class="nx">config</span><span class="p">.</span><span class="nx">callback</span><span class="p">();</span></div><div class='line' id='LC136'>					<span class="p">}</span></div><div class='line' id='LC137'>				<span class="p">}</span></div><div class='line' id='LC138'>				<span class="k">else</span> <span class="p">{</span></div><div class='line' id='LC139'>					<span class="c1">// Recurse</span></div><div class='line' id='LC140'>					<span class="nx">ScrollTo</span><span class="p">.</span><span class="nx">scroll</span><span class="p">(</span><span class="nx">collections</span><span class="p">,</span><span class="nx">config</span><span class="p">);</span></div><div class='line' id='LC141'>				<span class="p">}</span></div><div class='line' id='LC142'>				<span class="c1">// Return true</span></div><div class='line' id='LC143'>				<span class="k">return</span> <span class="kc">true</span><span class="p">;</span></div><div class='line' id='LC144'>			<span class="p">};</span></div><div class='line' id='LC145'><br/></div><div class='line' id='LC146'>			<span class="c1">// Handle if we only want to scroll if we are outside the viewport</span></div><div class='line' id='LC147'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">config</span><span class="p">.</span><span class="nx">onlyIfOutside</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC148'>				<span class="c1">// Determine current scroll positions</span></div><div class='line' id='LC149'>				<span class="nx">containerScrollTopEnd</span> <span class="o">=</span> <span class="nx">containerScrollTop</span> <span class="o">+</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">height</span><span class="p">();</span></div><div class='line' id='LC150'>				<span class="nx">containerScrollLeftEnd</span> <span class="o">=</span> <span class="nx">containerScrollLeft</span> <span class="o">+</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">width</span><span class="p">();</span></div><div class='line' id='LC151'><br/></div><div class='line' id='LC152'>				<span class="c1">// Check if we are in the range of the visible area of the container</span></div><div class='line' id='LC153'>				<span class="k">if</span> <span class="p">(</span> <span class="nx">containerScrollTop</span> <span class="o">&lt;</span> <span class="nx">targetOffsetTopAdjusted</span> <span class="o">&amp;&amp;</span> <span class="nx">targetOffsetTopAdjusted</span> <span class="o">&lt;</span> <span class="nx">containerScrollTopEnd</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC154'>					<span class="nx">targetOffsetTopAdjusted</span> <span class="o">=</span> <span class="nx">containerScrollTop</span><span class="p">;</span></div><div class='line' id='LC155'>				<span class="p">}</span></div><div class='line' id='LC156'>				<span class="k">if</span> <span class="p">(</span> <span class="nx">containerScrollLeft</span> <span class="o">&lt;</span> <span class="nx">targetOffsetLeftAdjusted</span> <span class="o">&amp;&amp;</span> <span class="nx">targetOffsetLeftAdjusted</span> <span class="o">&lt;</span> <span class="nx">containerScrollLeftEnd</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC157'>					<span class="nx">targetOffsetLeftAdjusted</span> <span class="o">=</span> <span class="nx">containerScrollLeft</span><span class="p">;</span></div><div class='line' id='LC158'>				<span class="p">}</span></div><div class='line' id='LC159'>			<span class="p">}</span></div><div class='line' id='LC160'><br/></div><div class='line' id='LC161'>			<span class="c1">// Determine the scroll options</span></div><div class='line' id='LC162'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">targetOffsetTopAdjusted</span> <span class="o">!==</span> <span class="nx">containerScrollTop</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC163'>				<span class="nx">scrollOptions</span><span class="p">.</span><span class="nx">scrollTop</span> <span class="o">=</span> <span class="nx">targetOffsetTopAdjusted</span><span class="p">;</span></div><div class='line' id='LC164'>			<span class="p">}</span></div><div class='line' id='LC165'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">targetOffsetLeftAdjusted</span> <span class="o">!==</span> <span class="nx">containerScrollLeft</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC166'>				<span class="nx">scrollOptions</span><span class="p">.</span><span class="nx">scrollLeft</span> <span class="o">=</span> <span class="nx">targetOffsetLeftAdjusted</span><span class="p">;</span></div><div class='line' id='LC167'>			<span class="p">}</span></div><div class='line' id='LC168'><br/></div><div class='line' id='LC169'>			<span class="c1">// Check to see if the scroll is necessary</span></div><div class='line' id='LC170'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">prop</span><span class="p">(</span><span class="s1">&#39;scrollHeight&#39;</span><span class="p">)</span> <span class="o">===</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">width</span><span class="p">()</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC171'>				<span class="k">delete</span> <span class="nx">scrollOptions</span><span class="p">.</span><span class="nx">scrollTop</span><span class="p">;</span></div><div class='line' id='LC172'>			<span class="p">}</span></div><div class='line' id='LC173'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">prop</span><span class="p">(</span><span class="s1">&#39;scrollWidth&#39;</span><span class="p">)</span> <span class="o">===</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">width</span><span class="p">()</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC174'>				<span class="k">delete</span> <span class="nx">scrollOptions</span><span class="p">.</span><span class="nx">scrollLeft</span><span class="p">;</span></div><div class='line' id='LC175'>			<span class="p">}</span></div><div class='line' id='LC176'><br/></div><div class='line' id='LC177'>			<span class="c1">// Perform the scroll</span></div><div class='line' id='LC178'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">scrollOptions</span><span class="p">.</span><span class="nx">scrollTop</span> <span class="o">!=</span> <span class="kc">null</span> <span class="o">||</span> <span class="nx">scrollOptions</span><span class="p">.</span><span class="nx">scrollLeft</span> <span class="o">!=</span> <span class="kc">null</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC179'>				<span class="nx">$container</span><span class="p">.</span><span class="nx">animate</span><span class="p">(</span><span class="nx">scrollOptions</span><span class="p">,</span> <span class="p">{</span></div><div class='line' id='LC180'>					<span class="nx">duration</span><span class="o">:</span> <span class="nx">config</span><span class="p">.</span><span class="nx">duration</span><span class="p">,</span></div><div class='line' id='LC181'>					<span class="nx">easing</span><span class="o">:</span> <span class="nx">config</span><span class="p">.</span><span class="nx">easing</span><span class="p">,</span></div><div class='line' id='LC182'>					<span class="nx">complete</span><span class="o">:</span> <span class="nx">callback</span></div><div class='line' id='LC183'>				<span class="p">});</span></div><div class='line' id='LC184'>			<span class="p">}</span></div><div class='line' id='LC185'>			<span class="k">else</span> <span class="p">{</span></div><div class='line' id='LC186'>				<span class="nx">callback</span><span class="p">();</span></div><div class='line' id='LC187'>			<span class="p">}</span></div><div class='line' id='LC188'><br/></div><div class='line' id='LC189'>			<span class="c1">// Return true</span></div><div class='line' id='LC190'>			<span class="k">return</span> <span class="kc">true</span><span class="p">;</span></div><div class='line' id='LC191'>		<span class="p">},</span></div><div class='line' id='LC192'><br/></div><div class='line' id='LC193'>		<span class="c1">// ScrollTo the Element using the Options</span></div><div class='line' id='LC194'>		<span class="nx">fn</span><span class="o">:</span> <span class="kd">function</span><span class="p">(</span><span class="nx">options</span><span class="p">){</span></div><div class='line' id='LC195'>			<span class="c1">// Prepare</span></div><div class='line' id='LC196'>			<span class="kd">var</span> <span class="nx">collections</span><span class="p">,</span> <span class="nx">config</span><span class="p">,</span> <span class="nx">$container</span><span class="p">,</span> <span class="nx">container</span><span class="p">;</span></div><div class='line' id='LC197'>			<span class="nx">collections</span> <span class="o">=</span> <span class="p">[];</span></div><div class='line' id='LC198'><br/></div><div class='line' id='LC199'>			<span class="c1">// Prepare</span></div><div class='line' id='LC200'>			<span class="kd">var</span>	<span class="nx">$target</span> <span class="o">=</span> <span class="nx">$</span><span class="p">(</span><span class="k">this</span><span class="p">);</span></div><div class='line' id='LC201'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">$target</span><span class="p">.</span><span class="nx">length</span> <span class="o">===</span> <span class="mi">0</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC202'>				<span class="c1">// Chain</span></div><div class='line' id='LC203'>				<span class="k">return</span> <span class="k">this</span><span class="p">;</span></div><div class='line' id='LC204'>			<span class="p">}</span></div><div class='line' id='LC205'><br/></div><div class='line' id='LC206'>			<span class="c1">// Handle Options</span></div><div class='line' id='LC207'>			<span class="nx">config</span> <span class="o">=</span> <span class="nx">$</span><span class="p">.</span><span class="nx">extend</span><span class="p">({},</span><span class="nx">ScrollTo</span><span class="p">.</span><span class="nx">config</span><span class="p">,</span><span class="nx">options</span><span class="p">);</span></div><div class='line' id='LC208'><br/></div><div class='line' id='LC209'>			<span class="c1">// Fetch</span></div><div class='line' id='LC210'>			<span class="nx">$container</span> <span class="o">=</span> <span class="nx">$target</span><span class="p">.</span><span class="nx">parent</span><span class="p">();</span></div><div class='line' id='LC211'>			<span class="nx">container</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">get</span><span class="p">(</span><span class="mi">0</span><span class="p">);</span></div><div class='line' id='LC212'><br/></div><div class='line' id='LC213'>			<span class="c1">// Cycle through the containers</span></div><div class='line' id='LC214'>			<span class="k">while</span> <span class="p">(</span> <span class="p">(</span><span class="nx">$container</span><span class="p">.</span><span class="nx">length</span> <span class="o">===</span> <span class="mi">1</span><span class="p">)</span> <span class="o">&amp;&amp;</span> <span class="p">(</span><span class="nx">container</span> <span class="o">!==</span> <span class="nb">document</span><span class="p">.</span><span class="nx">body</span><span class="p">)</span> <span class="o">&amp;&amp;</span> <span class="p">(</span><span class="nx">container</span> <span class="o">!==</span> <span class="nb">document</span><span class="p">)</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC215'>				<span class="c1">// Check Container for scroll differences</span></div><div class='line' id='LC216'>				<span class="kd">var</span> <span class="nx">containerScrollTop</span><span class="p">,</span> <span class="nx">containerScrollLeft</span><span class="p">;</span></div><div class='line' id='LC217'>				<span class="nx">containerScrollTop</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">css</span><span class="p">(</span><span class="s1">&#39;overflow-y&#39;</span><span class="p">)</span> <span class="o">!==</span> <span class="s1">&#39;visible&#39;</span> <span class="o">&amp;&amp;</span> <span class="nx">container</span><span class="p">.</span><span class="nx">scrollHeight</span> <span class="o">!==</span> <span class="nx">container</span><span class="p">.</span><span class="nx">clientHeight</span><span class="p">;</span></div><div class='line' id='LC218'>				<span class="nx">containerScrollLeft</span> <span class="o">=</span>  <span class="nx">$container</span><span class="p">.</span><span class="nx">css</span><span class="p">(</span><span class="s1">&#39;overflow-x&#39;</span><span class="p">)</span> <span class="o">!==</span> <span class="s1">&#39;visible&#39;</span> <span class="o">&amp;&amp;</span> <span class="nx">container</span><span class="p">.</span><span class="nx">scrollWidth</span> <span class="o">!==</span> <span class="nx">container</span><span class="p">.</span><span class="nx">clientWidth</span><span class="p">;</span></div><div class='line' id='LC219'>				<span class="k">if</span> <span class="p">(</span> <span class="nx">containerScrollTop</span> <span class="o">||</span> <span class="nx">containerScrollLeft</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC220'>					<span class="c1">// Push the Collection</span></div><div class='line' id='LC221'>					<span class="nx">collections</span><span class="p">.</span><span class="nx">push</span><span class="p">({</span></div><div class='line' id='LC222'>						<span class="s1">&#39;$container&#39;</span><span class="o">:</span> <span class="nx">$container</span><span class="p">,</span></div><div class='line' id='LC223'>						<span class="s1">&#39;$target&#39;</span><span class="o">:</span> <span class="nx">$target</span></div><div class='line' id='LC224'>					<span class="p">});</span></div><div class='line' id='LC225'>					<span class="c1">// Update the Target</span></div><div class='line' id='LC226'>					<span class="nx">$target</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">;</span></div><div class='line' id='LC227'>				<span class="p">}</span></div><div class='line' id='LC228'>				<span class="c1">// Update the Container</span></div><div class='line' id='LC229'>				<span class="nx">$container</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">parent</span><span class="p">();</span></div><div class='line' id='LC230'>				<span class="nx">container</span> <span class="o">=</span> <span class="nx">$container</span><span class="p">.</span><span class="nx">get</span><span class="p">(</span><span class="mi">0</span><span class="p">);</span></div><div class='line' id='LC231'>			<span class="p">}</span></div><div class='line' id='LC232'><br/></div><div class='line' id='LC233'>			<span class="c1">// Add the final collection</span></div><div class='line' id='LC234'>			<span class="nx">collections</span><span class="p">.</span><span class="nx">push</span><span class="p">({</span></div><div class='line' id='LC235'>				<span class="s1">&#39;$container&#39;</span><span class="o">:</span> <span class="nx">$</span><span class="p">(</span><span class="s1">&#39;html&#39;</span><span class="p">),</span></div><div class='line' id='LC236'>				<span class="c1">// document.body doesn&#39;t work in firefox, html works for all</span></div><div class='line' id='LC237'>				<span class="c1">// internet explorer starts at the beggining</span></div><div class='line' id='LC238'>				<span class="s1">&#39;$target&#39;</span><span class="o">:</span> <span class="nx">$target</span></div><div class='line' id='LC239'>			<span class="p">});</span></div><div class='line' id='LC240'><br/></div><div class='line' id='LC241'>			<span class="c1">// Adjust the Config</span></div><div class='line' id='LC242'>			<span class="k">if</span> <span class="p">(</span> <span class="nx">config</span><span class="p">.</span><span class="nx">durationMode</span> <span class="o">===</span> <span class="s1">&#39;all&#39;</span> <span class="p">)</span> <span class="p">{</span></div><div class='line' id='LC243'>				<span class="nx">config</span><span class="p">.</span><span class="nx">duration</span> <span class="o">/=</span> <span class="nx">collections</span><span class="p">.</span><span class="nx">length</span><span class="p">;</span></div><div class='line' id='LC244'>			<span class="p">}</span></div><div class='line' id='LC245'><br/></div><div class='line' id='LC246'>			<span class="c1">// Handle</span></div><div class='line' id='LC247'>			<span class="nx">ScrollTo</span><span class="p">.</span><span class="nx">scroll</span><span class="p">(</span><span class="nx">collections</span><span class="p">,</span><span class="nx">config</span><span class="p">);</span></div><div class='line' id='LC248'><br/></div><div class='line' id='LC249'>			<span class="c1">// Chain</span></div><div class='line' id='LC250'>			<span class="k">return</span> <span class="k">this</span><span class="p">;</span></div><div class='line' id='LC251'>		<span class="p">}</span></div><div class='line' id='LC252'>	<span class="p">};</span></div><div class='line' id='LC253'><br/></div><div class='line' id='LC254'>	<span class="c1">// Apply our extensions to jQuery</span></div><div class='line' id='LC255'>	<span class="nx">$</span><span class="p">.</span><span class="nx">ScrollTo</span> <span class="o">=</span> <span class="nx">$</span><span class="p">.</span><span class="nx">ScrollTo</span> <span class="o">||</span> <span class="nx">ScrollTo</span><span class="p">;</span></div><div class='line' id='LC256'>	<span class="nx">$</span><span class="p">.</span><span class="nx">fn</span><span class="p">.</span><span class="nx">ScrollTo</span> <span class="o">=</span> <span class="nx">$</span><span class="p">.</span><span class="nx">fn</span><span class="p">.</span><span class="nx">ScrollTo</span> <span class="o">||</span> <span class="nx">ScrollTo</span><span class="p">.</span><span class="nx">fn</span><span class="p">;</span></div><div class='line' id='LC257'><br/></div><div class='line' id='LC258'>	<span class="c1">// Export</span></div><div class='line' id='LC259'>	<span class="k">return</span> <span class="nx">ScrollTo</span><span class="p">;</span></div><div class='line' id='LC260'><span class="p">}).</span><span class="nx">call</span><span class="p">(</span><span class="k">this</span><span class="p">);</span></div></pre></div></td>
          </tr>
        </table>
  </div>

  </div>
</div>

<a href="#jump-to-line" rel="facebox[.linejump]" data-hotkey="l" class="js-jump-to-line" style="display:none">Jump to Line</a>
<div id="jump-to-line" style="display:none">
  <form accept-charset="UTF-8" class="js-jump-to-line-form">
    <input class="linejump-input js-jump-to-line-field" type="text" placeholder="Jump to line&hellip;" autofocus>
    <button type="submit" class="button">Go</button>
  </form>
</div>

        </div>

      </div><!-- /.repo-container -->
      <div class="modal-backdrop"></div>
    </div><!-- /.container -->
  </div><!-- /.site -->


    </div><!-- /.wrapper -->

      <div class="container">
  <div class="site-footer">
    <ul class="site-footer-links right">
      <li><a href="https://status.github.com/">Status</a></li>
      <li><a href="http://developer.github.com">API</a></li>
      <li><a href="http://training.github.com">Training</a></li>
      <li><a href="http://shop.github.com">Shop</a></li>
      <li><a href="/blog">Blog</a></li>
      <li><a href="/about">About</a></li>

    </ul>

    <a href="/">
      <span class="mega-octicon octicon-mark-github" title="GitHub"></span>
    </a>

    <ul class="site-footer-links">
      <li>&copy; 2014 <span title="0.05189s from github-fe123-cp1-prd.iad.github.net">GitHub</span>, Inc.</li>
        <li><a href="/site/terms">Terms</a></li>
        <li><a href="/site/privacy">Privacy</a></li>
        <li><a href="/security">Security</a></li>
        <li><a href="/contact">Contact</a></li>
    </ul>
  </div><!-- /.site-footer -->
</div><!-- /.container -->


    <div class="fullscreen-overlay js-fullscreen-overlay" id="fullscreen_overlay">
  <div class="fullscreen-container js-fullscreen-container">
    <div class="textarea-wrap">
      <textarea name="fullscreen-contents" id="fullscreen-contents" class="js-fullscreen-contents" placeholder="" data-suggester="fullscreen_suggester"></textarea>
    </div>
  </div>
  <div class="fullscreen-sidebar">
    <a href="#" class="exit-fullscreen js-exit-fullscreen tooltipped tooltipped-w" aria-label="Exit Zen Mode">
      <span class="mega-octicon octicon-screen-normal"></span>
    </a>
    <a href="#" class="theme-switcher js-theme-switcher tooltipped tooltipped-w"
      aria-label="Switch themes">
      <span class="octicon octicon-color-mode"></span>
    </a>
  </div>
</div>



    <div id="ajax-error-message" class="flash flash-error">
      <span class="octicon octicon-alert"></span>
      <a href="#" class="octicon octicon-remove-close close js-ajax-error-dismiss"></a>
      Something went wrong with that request. Please try again.
    </div>

  </body>
</html>


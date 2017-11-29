window.ecms = (function () {
    var result = {};

    result.rootPath = '/',

    result.state = {
        mode: 'edit',
        changeMode: function (m) {
            this.mode = m;

            if (this.isInEditMode()) {
                $('[data-ecms-tag]').each(result.nodesAttachEvents);
            }
            else {
                $('[data-ecms-tag]').each(result.nodesDetachEvents);
            }
        },
        isInEditMode: function () {
            return this.mode == 'edit';
        }
    },

    result.resources = {
        Text_Confirm_Delete: 'Are you sure you want to delete?',
        Text_Ajax_Loading: 'Loading...',
        Text_ImageSelector_EnterUrl: 'Enter URL:',
        Text_ImageSelector_Select: 'Select',
        Text_ImageSelector_Submit: 'Submit',
        Titles_Repeater_NewItem: 'New item',
        Titles_PageData: 'Page data',
        Titles_ChangePassword: 'Change password',
        Titles_Login: 'Login',
        Titles_EditItem: 'Edit - ',
        Buttons_Save: 'Save',
        Titles_Rename: 'Rename',
        Titles_CreateFolder: 'Create folder',
        Titles_OpenFile: 'Open file - ',
        Titles_ServerExplorer: 'Server explorer',
        Titles_Upload: 'Upload'
    },

    result.init = function () {
        var selectedMode = $('input[name=editmode]:checked');
        if (selectedMode.length > 0) {
            result.state.changeMode(selectedMode.attr('id'));
        }

        if (result.state.isInEditMode()) {
            $('[data-ecms-tag]').each(result.nodesAttachEvents);
        }

        $(document.body).append("<div id='ajaxLoader'><div class='overlay'></div><div class='loading'>" + result.resources.Text_Ajax_Loading + "</div></div>");

        result.ajaxLoader = $('#ajaxLoader').each(
            function () {
                var loader = $(this);
                result.showAjaxLoader = function () { loader.show(); };
            }
        )
        .ajaxStart(function () {
            result.ajaxLoaderTimeout = setTimeout('window.ecms.showAjaxLoader()', 200);
        })
        .ajaxStop(function () {
            clearTimeout(result.ajaxLoaderTimeout);
            $(this).hide();
        });

        $.cleditor.buttons.image.buttonClick = function (e, data) {
            result.createButtons($(data.popup));
        }
        $.cleditor.buttons.link.buttonClick = function (e, data) {
            result.createButtons($(data.popup));
        }

        $.cleditor.buttons.image.popupClass = 'cleditorPrompt';
        $.cleditor.buttons.image.popupContent = result.resources.Text_ImageSelector_EnterUrl + "<br><input type=text value='http://' size=35><button data-ecms-icon='ui-icon-image' onclick='ecms.onCLeditorImageSelect(this)'>" + result.resources.Text_ImageSelector_Select + "</button><br><input type=button value='" + result.resources.Text_ImageSelector_Submit + "'>";
    }

    result.checkCanDelete = function () {
        return confirm(result.resources.Text_Confirm_Delete);
    },

    result.nodesAttachEvents = function (i, obj) {
        var $obj = $(obj);
        var eventData = result.fillEditorEventData(obj);
        var border = $obj.css('border');
        obj.ecmsRestoreCss = function () {
            $obj.removeClass('redBorder');
            $obj.css('border', border);
            if (border == null && $.browser.msie) {
                $obj.css("borderColor", "");
                $obj.css("borderWidth", "");
                $obj.css("borderStyle", "");
            }
        }

        $obj.addClass('redBorder');

        if (eventData.ecmsTagSettings.length > 1) {
            $obj.unbind('click', result.showEditor);
            $obj.click(eventData, result.showEditor);
        }
    },

    result.nodesDetachEvents = function (i, obj) {
        if (obj.ecmsRestoreCss != null) {
            obj.ecmsRestoreCss();
        }
        $(obj).unbind('click', result.showEditor);
    },

    result.editPageDataItem = function (key, sender) {
        var keySelector = '[data-ecms-tag="' + key + '"]';

        var obj = $('[data-ecms-tag="' + key + '"]').get(0);

        if (obj != null) {
            var eventData = result.fillEditorEventData(obj);
            eventData.onSaved = function () {
                var formData = 'pageId=' + result.getPageId();
                formData = formData + '&id=' + key;

                $.ajax(
                {
                    url: result.rootPath + 'ECmsData/UpdatePageData',
                    type: 'POST',
                    data: formData,
                    cache: false,
                    success: function (data) {
                        $('#ajaxLoader').hide();
                        $(sender).parent().prev('td').find('.dataValueData').html(data);
                    },
                    error: result.processAjaxError
                });
            };

            result.showEditor({ data: eventData });
        }
    },

    result.clearPageDataItem = function (key, sender) {
        if (ecms.checkCanDelete()) {
            var ecmsTagSettings = key.split('|');
            var pageId = result.getPageId();
            if (ecmsTagSettings.length > 2) {
                pageId = ecmsTagSettings[2];
            }

            var url = result.rootPath + 'ecms' + ecmsTagSettings[0] + '/clear?id=' + ecmsTagSettings[1] + '&pageId=' + pageId + '&location=' + location;

            var keySelector = '[data-ecms-tag="' + key + '"]';

            $.ajax(
            {
                url: url,
                type: 'POST',
                data: null,
                cache: false,
                success: function (data) {
                    $('#ajaxLoader').hide();
                    if (data.Successed) {
                        $(keySelector).each(
                            function (i, obj) {
                                var objData = $(data.Views[i]);
                                $(obj).replaceWith(objData);
                                if (result.state.isInEditMode()) {
                                    $(objData).each(result.nodesAttachEvents);
                                }
                            }
                         );
                        $(sender).parent().prev('td').prev('td').html('');
                    }
                    else {
                        alert(data.Message);
                    }
                },
                error: result.processAjaxError
            });
        }
    },

    result.fillEditorEventData = function (obj) {
        var ecmsTag = $(obj).attr('data-ecms-tag');
        var ecmsTagSettings = ecmsTag.split('|');
        return { ecmsTag: ecmsTag, ecmsTagSettings: ecmsTagSettings, obj: obj, ecmsCustomAttributes: $(obj).attr('data-ecms-attributes') };
    },

    result.showEditor = function (e) {
        var pageId = result.getPageId();
        if (e.data.ecmsTagSettings.length > 2) {
            pageId = e.data.ecmsTagSettings[2];
        }

        var url = result.rootPath + 'ecms' + e.data.ecmsTagSettings[0] + '/edit?id=' + e.data.ecmsTagSettings[1] + '&pageId=' + pageId + '&location=' + location;

        if (e.data.ecmsCustomAttributes) {
            url += '&attributes=' + e.data.ecmsCustomAttributes;
        }

        var formMethod = "GET";
        var formData = null;

        if (e.data.ecmsTagSettings[0] == 'repeater' || e.data.ecmsTagSettings[0] == 'dataset') {
            formMethod = "POST";
            formData = "items=" + $(e.data.obj).attr('data-ecms-items');
        }

        ecms.callDialogAction(url, formMethod, formData, e.data.ecmsTagSettings[1], function (data) {
            if (data != null) {
                if (data.Successed) {
                    $('[data-ecms-tag="' + e.data.ecmsTag + '"]').each(function (index, obj) {
                        $(obj).replaceWith(data.Views[index]);
                    });

                    if (result.state.isInEditMode()) {
                        $('[data-ecms-tag="' + e.data.ecmsTag + '"]').each(result.nodesAttachEvents)
                                                                 .find('[data-ecms-tag]').each(result.nodesAttachEvents);

                    }
                    if (typeof (e.data.onSaved) == 'function') {
                        e.data.onSaved();
                    }

                    return true;
                }
                else {
                    alert(data.Message);
                }
            }

            return false;
        });

        return false;
    },

    result.editRepeaterItem = function (itemPrefix, rowIndex, itemsId, parentPrefix) {
        var dataHolder = $('[id="' + itemPrefix + '_line"]');
        var postData = $(document.getElementById(result.nameToId(itemsId))).serialize();
        postData += '&' + $(document.getElementById(itemPrefix + '_item')).find('*').serialize();
        postData += '&prefix=' + parentPrefix + '&rowIndex=' + rowIndex;

        ecms.callDialogAction(result.rootPath + 'ECmsRepeater/EditItem', 'POST', postData, rowIndex, function (data) {
            if (data != null) {
                dataHolder.replaceWith(data);
                var item = $('[id="' + itemPrefix + '_line"]');
                result.createButtons(item);
                result.updateRepeaterItemsPositions(item.parent());
                return true;
            } return false;
        });
    },

    result.addRepeaterItem = function (sender, parentPrefix) {
        var linesContainer = sender.parent().children('div.repeaterLines');
        var lastIndex = linesContainer.children('div').length;

        var postData = $(document.getElementById(result.nameToId(parentPrefix) + '_items')).serialize();
        postData += '&prefix=' + parentPrefix + '&rowIndex=' + lastIndex;

        result.callDialogAction(result.rootPath + 'ECmsRepeater/AddItem', 'POST', postData, result.resources.Titles_Repeater_NewItem,
        function (data) {
            if (data != null) {
                var newLine = $(data);
                linesContainer.append(newLine);
                result.createButtons(newLine);
                result.updateRepeaterMaxRowIndex(linesContainer.parent());
                return true;
            }

            return false;
        });
    },

    result.deleteRepeaterItem = function (sender) {
        var $sender = $(sender);
        result.updateRepeaterMaxRowIndex($sender.closest('.repeaterList'));

        $sender.parent().remove()
    },

    result.updateRepeaterMaxRowIndex = function (repeater) {
        var maxIndex = 0;
        repeater.children('.repeaterLines').children('.ecmsRpRow').each(function (index, row) {
            var rowIndex = parseInt($(row).attr('data-ecms-row'));
            if (rowIndex > maxIndex) {
                maxIndex = rowIndex;
            }
        });

        repeater.children('input').last().val(maxIndex + 1);
    },

    result.updateRepeaterItemsPositions = function (repeaterLines) {
        repeaterLines.children('div').each(function (index, obj) {
            $(obj).children('input.position').val(index);
        });
    },

    result.getPageData = function () {
        var postData = 'pageId=' + result.getPageId();
        $('[data-ecms-tag]').each(function (i, obj) {
            postData = postData + '&' + 'ids[' + i + ']=' + $(obj).attr('data-ecms-tag');
        });

        result.callDialogAction(result.rootPath + 'ECmsData/GetPageData', 'POST', postData, result.resources.Titles_PageData, function (data) { return true; }, true);
    },

    result.getPageId = function () {
        var location = window.location.href;
        var queryIndex = location.indexOf('?');
        if (queryIndex > 0) {
            location = location.substring(0, queryIndex);
        }

        return location;
    },

    result.nameToId = function (name) {
        return name.replace(/[\[\.\]]/ig, '_');
    },

    result.showExtraProperties = function (sender) {
        $(sender).prev('div.hidden').dialog(
        { closeText: 'x',
            title: 'Properties',
            modal: true,
            resizable: false,
            width: 'auto',
            draggable: true,
            close: function () { $(sender).before($(this).unbind('keydown').remove()); },
            buttons: { 'Save': function () { $(this).dialog('close'); } }
        }).bind("keydown", result.dialogOnEnter);
    },

    result.changePassword = function () {
        result.callDialogAction(result.rootPath + 'ECmsAccount/ChangePassword', 'GET', null, result.resources.Titles_ChangePassword, function (data) {
            return data.success;
        }, false, true);
    },

    result.showLoginPopUp = function () {
        result.callDialogAction(result.rootPath + 'ECmsAccount/Login', 'GET', null, result.resources.Titles_Login, function (data) {
            if (data.success) {
                $("div.adminPanel").show().trigger('login');
            }
            return data.success;
        }, false, true);
    },

    result.callDialogAction = function (url, formMethod, formData, edtiTitle, onComplete, hideButtons, fullTitle, onClose) {
        if (result.showingDialog) {
            return;
        }

        $.ajax(
            {
                url: url,
                type: formMethod,
                data: formData,
                cache: false,
                success: function (data) {
                    $('#ajaxLoader').hide();
                    result.buildDialog(data, fullTitle ? edtiTitle : result.resources.Titles_EditItem + edtiTitle, onComplete, hideButtons, onClose);
                    result.showingDialog = false;
                },
                error: result.processAjaxError
            });
    },

    result.processAjaxError = function (response) {
        if (response.status == 403) {
            result.showLoginPopUp();
        }

        result.showingDialog = false;
    },

    result.buildDialog = function (data, edtiTitle, onComplete, hideButtons, onClose) {
        var successName = 'ecmsSuccess' + new Date().getTime();
        var $data = $(data);
        var $content = $data.not('script');
        var $detachedChildren = $content.children().detach();
        eval("var buttonsSet = { '" + result.resources.Buttons_Save + "': function () { $(this).parent().find('form').submit(); } }");

        $content.dialog({ closeText: 'x', title: edtiTitle, modal: true, resizable: false, width: 'auto', draggable: true,
            close: function () {
                $(this).unbind('keydown').remove();
                $(this).find('.repeaterLines.ui-sortable').sortable('destroy');
                eval("window." + successName + " = null;");
                if (onClose != null) {
                    onClose(this);
                }
            },
            open: function () {
                $detachedChildren.appendTo($content);

                var $form = $(this);
                $form.css('position', 'static');
                $form.addClass('cmsForm');
                var successFunction = function (data) {
                    $form.dialog('close');
                    eval("window." + successName + " = null;");

                    if (!onComplete(data)) {
                        result.buildDialog(data, edtiTitle, onComplete, hideButtons);
                    }
                };

                eval('window.' + successName + ' = successFunction;');

                $form.attr('data-ajax-success', successName);
                $.validator.unobtrusive.parse(this);
                $form.validate($form.data('unobtrusiveValidation').options);
                $form.find('textarea:visible').cleditor({ width: '500px' }).bind("keydown", function () { $form.find('textarea')[0].stopPropagation() });
                result.createButtons($form);

                result.center($(this).closest('.ui-dialog'));
                $form.bind("keydown", result.dialogOnEnter);

                $form.find('input[type=text],textarea,select').filter(':visible:first').focus();
            },
            buttons: hideButtons ? {} : buttonsSet
        });

        $data.not($content).appendTo(document.body);
    },

    result.createButtons = function (holder) {
        holder.find('button:visible').each(
                function () {
                    $(this).button({
                        icons: {
                            primary: $(this).attr('data-ecms-icon')
                        },
                        text: false
                    }).click(function () { $(this).removeClass('ui-state-focus'); });
                });
        holder.find('.repeaterLines:visible').sortable({
            stop: function (event, ui) {
                result.updateRepeaterItemsPositions($(ui.item).parent());
            }
        });

        holder.find('.tabs:visible').tabs();
    },

    result.center = function ($element) {
        var top = ($(window).height() - $element.outerHeight()) / 2;
        var left = ($(window).width() - $element.outerWidth()) / 2;
        $element.css({ position: 'absolute', margin: 0, top: (top > 0 ? top : 0) + 'px', left: (left > 0 ? left : 0) + 'px' });
    },

    result.dialogOnEnter = function (event) {
        var keycode = (event.keyCode ? event.keyCode : (event.which ? event.which : event.charCode));
        if (keycode == 13) {
            $(this).parent().find('div.ui-dialog-buttonpane button').click();
            return false;
        } else {
            return true;
        }
    },

    result.onCLeditorImageSelect = function (sender) {
        result.SE.showServerExplorerPopUp(function (selected) {
            var $parent = $(sender).parent();
            $parent.find(':text').val(selected);
            $parent.find('[value=Submit]').click();
        });

        return false;
    },

    result.onImageSelect = function (sender) {
        result.SE.showServerExplorerPopUp(function (selected) { sender.val(selected); });
    },

    result.SE = {
        actions: {
            'ecmsdelete': function (t) {
                var $item = $(t);
                if (result.checkCanDelete()) {
                    var actionUrl = result.rootPath + 'ECmsServerExplorer/Delete?directoryPath=' + encodeURIComponent($item.attr('data-content-path'));
                    var isFolder = false;
                    if ($item.attr('data-content-type') == 'folder') {
                        actionUrl += '&isFolder=true';
                        isFolder = true;
                    }

                    $.ajax({
                        url: actionUrl,
                        type: 'GET',
                        success: function (data) {
                            var path = data == null ? '' : data;
                            if (isFolder) {
                                var $li = result.SE.findTreeNode($item.attr('data-content-path')).closest('li');
                                if ($li.hasClass('last')) {
                                    if ($li.prev().length > 0) {
                                        $li.prev().addClass('last');
                                    }
                                    else {
                                        var $parentLi = $li.parent().closest('li');
                                        $parentLi.find('.minus').remove();
                                        $parentLi.find('.plus').remove();
                                    }
                                }

                                $li.remove();
                            }
                            result.SE.findTreeNode(data.path).click();
                        },
                        cache: false,
                        error: result.processAjaxError
                    });
                }
            },
            'ecmsrename': function (t) {
                var $item = $(t);
                var actionUrl = result.rootPath + 'ECmsServerExplorer/RenameDialog?path=' + encodeURIComponent($item.attr('data-content-path'));
                var isFolder = false;

                if ($item.attr('data-content-type') == 'folder') {
                    actionUrl += '&isFolder=true';
                    isFolder = true;
                }
                result.callDialogAction(actionUrl, 'GET', null, result.resources.Titles_Rename, function (data) {
                    if (data.success) {
                        if (isFolder) {
                            result.SE.refreshTree(data.path);
                        }
                        else {
                            result.SE.findTreeNode(data.path).click();
                        }

                        return true;
                    }

                    return false;
                }, false, true);
            },
            'ecmscreate': function (t) {
                result.callDialogAction(result.rootPath + 'ECmsServerExplorer/CreateDialog?path=' + encodeURIComponent(result.SE.getSelectedPath()), 'GET', null, result.resources.Titles_CreateFolder, function (data) {
                    if (data.success) {
                        result.SE.refreshTree(data.path);
                        return true;
                    }

                    return false;
                }, false, true);
            },
            'ecmsupload': function (t) {
                result.SE.showUploadPopUp();
            },
            'ecmsopen': function (t) {
                var item = $(t);
                var path = item.attr('data-content-path');

                if (item.attr('data-content-type') == 'folder') {
                    result.SE.findTreeNode(path).click().closest('ul.subTree').show();
                }
                else {
                    result.callDialogAction(result.rootPath + 'ECmsServerExplorer/OpenFile?filePath=' + encodeURIComponent(path), 'GET', null, result.resources.Titles_OpenFile + item.attr('data-content-name'), function (data) { }, true, true);
                }
            },
            'ecmsselect': function (t) {
                if (result.onEcmsSEClientSelect != null) {
                    var $item = $(t);
                    result.onEcmsSEClientSelect($item.attr('data-content-virtualpath'));
                    $item.closest('form').dialog('destroy').remove();
                }
            }
        },

        showServerExplorerPopUp: function (onClientSelect) {
            result.onEcmsSEClientSelect = onClientSelect;
            result.callDialogAction(result.rootPath + 'ECmsServerExplorer/ServerExplorer?selectable=' + (onClientSelect == null ? 'false' : 'true'), 'GET', null, result.resources.Titles_ServerExplorer, function (data) { }, true, true, function () { $('#jqContextMenu').hide().next('div').hide(); });
        },

        showUploadPopUp: function () {
            result.callDialogAction(result.rootPath + 'ECmsServerExplorer/ShowUploadPopup?explorerPath=' + encodeURIComponent(result.SE.getSelectedPath()), 'GET', null, result.resources.Titles_Upload, function (data) { }, false, true);
        },

        getSelectedPath: function () {
            return $('#ecmsSETree span.selected').attr('data-content-path');
        },

        findTreeNode: function (path) {
            var treeNodes = $('#ecmsSETree').find('span[data-content-path]');
            for (var i = 0; i < treeNodes.length; i++) {
                if ($(treeNodes[i]).attr('data-content-path') == path) {
                    return $(treeNodes[i]);
                }
            }

            return null;
        },

        onFileUploaded: function (path) {
            result.ajaxLoader.trigger('ajaxStop');

            var selectedNode = result.SE.findTreeNode(path);
            if (selectedNode != null) {
                selectedNode.click();
            }

            $('form#ecmsSEUpload').dialog('destroy').remove();
        },

        contentHover: function () {
            var contentRows = $("#ecmsSEContent  table  tr.contentRow");

            contentRows.hover(function () { $(this).addClass('hover'); }, function () { $(this).removeClass('hover'); });
            contentRows.click(function () {
                contentRows.removeClass('selected');
                result.SE.initTopActions($(this).addClass('selected'));
            });
        },

        initTree: function () {
            $("span[data-content-type]").each(function (i, obj) { result.SE.initFolderClick($(obj)); result.SE.initTreeNode(i, obj); });
            $('#ecmsSETopActions li').unbind('click').click(function () {
                eval('var action = ecms.SE.actions.' + this.id.substring(0, this.id.length - 4));
                action(result.ecmsSELastSelected);
            });

            var $selected = $('#ecmsSETree span.selected');
            if ($selected.length > 0) {
                result.SE.initTopActions($selected);
            }
        },

        initContextMenuForContent: function () {
            $("#ecmsSEContent  table  tr").each(result.SE.initTreeNode);
            result.SE.initTreeNode(0, $("#ecmsSEContent"));
        },

        initTreeNode: function (i, obj) {
            var treeNode = $(obj);

            treeNode.contextMenu('ecmsSEContextMenu', {
                bindings: result.SE.actions,
                onContextMenu: function (e) {
                    var $target = $(e.target);
                    result.SE.applyActions($target, $('#' + this.id), '');
                    $target.click();
                    return true;
                }
            });
        },

        applyActions: function ($element, $container, suffix) {
            var actions = $element.attr('data-content-actions');
            if (actions == null || actions == '') {
                actions = $element.closest('[data-content-actions]').attr('data-content-actions');
            }

            $container.find('li').addClass('hidden');

            $(actions.split(',')).each(function (i, obj) {
                $container.find('li#ecms' + obj + suffix).removeClass('hidden');
            });
        },

        initTopActions: function ($element) {
            var $container = $('#ecmsSETopActions');
            result.SE.applyActions($element, $container, '_top');
            result.ecmsSELastSelected = $element.get(0);
        },

        initFolderClick: function (treeNode) {
            treeNode.unbind('click');

            treeNode.click(function () {
                result.SE.initTopActions(treeNode);
                $('#ecmsSETree span.selected').removeClass('selected');
                treeNode.parent().find('.nodeTitle').addClass('selected');
                var path = treeNode.attr('data-content-path');
                var lastSelectedNodeCookie = "lastSelectedNode=" + escape(path) + "; path=/";

                document.cookie = lastSelectedNodeCookie;

                $.ajax({
                    url: result.rootPath + 'ECmsServerExplorer/Content?directoryPath=' + encodeURIComponent(path) + '&selectable=' + (result.onEcmsSEClientSelect != null),
                    type: 'GET',
                    success: function (data) {
                        $('#ecmsSEContent').html(data);
                        result.SE.initContextMenuForContent();
                    },
                    cache: false,
                    error: result.processAjaxError
                });
            });
        },

        refreshTree: function (path) {
            $.ajax({
                url: result.rootPath + 'ECmsServerExplorer/RefreshTree?directoryPath=' + encodeURIComponent(path),
                type: 'GET',
                cache: false,
                success: function (data) {
                    var treeNode = result.SE.findTreeNode(path);
                    treeNode.closest('li').replaceWith(data);

                    result.SE.initTree();
                    result.SE.initTreeToggle();

                    treeNode = result.SE.findTreeNode(path);
                    treeNode.click().prev().click();
                },
                error: result.processAjaxError
            });
        },

        initTreeToggle: function () {
            $('span.plus').unbind('click').click(function () {
                $(this).parent().parent().find('ul.subTree:first').toggle();

                if ($(this).hasClass('plus')) {
                    $(this).removeClass('plus');
                    $(this).addClass('minus');

                    return;
                }

                if ($(this).hasClass('minus')) {
                    $(this).removeClass('minus');
                    $(this).addClass('plus');

                    return;
                }
            });

            $('span.minus').unbind('click').click(function () {
                $(this).parent().parent().find('ul.subTree:first').toggle();

                if ($(this).hasClass('plus')) {
                    $(this).removeClass('plus');
                    $(this).addClass('minus');

                    return;
                }

                if ($(this).hasClass('minus')) {
                    $(this).removeClass('minus');
                    $(this).addClass('plus');

                    return;
                }
            });
        }
    }

    return result;
})();

$(window.ecms.init);
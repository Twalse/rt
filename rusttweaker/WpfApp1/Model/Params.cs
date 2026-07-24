using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfApp1.Model
{
	// Token: 0x02000066 RID: 102
	public static class Params
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00014D33 File Offset: 0x00013133
		public static List<Params.Node> ConvertToNodes([TupleElementNames(new string[] { "Key", "Value" })] List<ValueTuple<string, string>> list)
		{
			if (list == null)
			{
				return new List<Params.Node>();
			}
			return list.Select<ValueTuple<string, string>, Params.Node>(([TupleElementNames(new string[] { "Key", "Value" })] ValueTuple<string, string> x) => new Params.Node(x.Item1, x.Item2)).ToList<Params.Node>();
		}

		// Token: 0x04000103 RID: 259
		public static Dictionary<string, Params.TweakNode> Tweaks = new Dictionary<string, Params.TweakNode>
		{
			{
				"trash_settings",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("client.headbob", "\"False\""),
					new Params.Node("client.hurtpunch", "\"False\""),
					new Params.Node("global.showblood", "\"False\""),
					new Params.Node("global.censorrecordings", "\"False\""),
					new Params.Node("shoutcaststreamer.allowinternetstreams", "\"False\""),
					new Params.Node("effects.hurtoverlay", "\"False\""),
					new Params.Node("effects.hurtoverleyapplylighting", "\"False\""),
					new Params.Node("effects.bloom", "\"False\""),
					new Params.Node("effects.shafts", "\"False\""),
					new Params.Node("effects.lensdirt", "\"False\""),
					new Params.Node("graphics.branding", "\"False\""),
					new Params.Node("gametip.showgametips", "\"False\""),
					new Params.Node("graphicssettings.particleraycastbudget", "\"0\""),
					new Params.Node("graphicssettings.pixellightcount", "\"0\""),
					new Params.Node("ui.showbeltbarbinds", "\"False\""),
					new Params.Node("water.quality", "\"0\""),
					new Params.Node("effects.vignet", "\"False\""),
					new Params.Node("global.processmidiinput", "\"False\""),
					new Params.Node("player.cold_breath", "\"False\""),
					new Params.Node("client.hascompletedtutorial", "\"True\""),
					new Params.Node("render.instanced_rendering", "\"0\""),
					new Params.Node("graphicssettings.billboardsfacecameraposition", "\"False\"")
				})
			},
			{
				"legs_vision",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("legs.enablelegs", "\"False\"")
				})
			},
			{
				"shake_cam",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("client.clampscreenshake", "\"True\""),
					new Params.Node("client.allowcameratiltondpv", "\"False\""),
					new Params.Node("client.headbob", "\"False\""),
					new Params.Node("client.hurtpunch", "\"False\"")
				})
			},
			{
				"cross_on_the_threes",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("accessibility.treemarkercolor", "\"2\"")
				})
			},
			{
				"disable_secure_occlusion",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("culling.safemode", "\"False\"")
				})
			},
			{
				"disable_the_wreckage",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("effects.maxgibdist", "\"150\""),
					new Params.Node("effects.maxgibs", "\"0\""),
					new Params.Node("effects.maxgiblife", "\"0\""),
					new Params.Node("effects.mingiblife", "\"0\"")
				})
			},
			{
				"disable_eyes_animation",
				new Params.TweakNode(null, new Params.Node[]
				{
					new Params.Node("player.eye_blinking", "\"False\""),
					new Params.Node("player.eye_movement", "\"False\"")
				})
			},
			{
				"disable_leg_deformity",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("player.footik", "\"False\"")
				})
			},
			{
				"disable_stroboscope",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("strobelight.forceoff", "\"True\"")
				})
			},
			{
				"fast_head_rotate",
				new Params.TweakNode(null, new Params.Node[]
				{
					new Params.Node("client.headlerp", "\"10\""),
					new Params.Node("headlerp_inertia", "\"0\"")
				})
			},
			{
				"return_events_textannounce",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("ui.monumentnotificationtoasts", "\"True\"")
				})
			},
			{
				"disable_craft_delay",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("inventory.quickcraftdelay", "\"0\"")
				})
			},
			{
				"smalltime_bag_unclaim",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("client.bag_unclaim_duration", "\"0.1\"")
				})
			},
			{
				"add_map_info",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("debug.showworldinfoinperformancereadout", "\"True\"")
				})
			},
			{
				"disable_show_errors",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("console.erroroverlay", "\"False\"")
				})
			},
			{
				"add_admin_gestures",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("gesturecollection.showadmincinematicgesturesinbindings", "\"True\"")
				})
			},
			{
				"server_hitmarks",
				new Params.TweakNode(null, new Params.Node[]
				{
					new Params.Node("hitnotify.notification_level", "\"2\"")
				})
			},
			{
				"old_announce_for_take_item",
				new Params.TweakNode(null, new Params.Node[]
				{
					new Params.Node("global.showitempickupnotices", "\"1\""),
					new Params.Node("global.showitemcountsonpickup", "\"False\""),
					new Params.Node("global.usesingleitempickupnotice", "\"False\"")
				})
			},
			{
				"small_time_use_menu",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("input.holdtime", "\"0.15\"")
				})
			},
			{
				"admin_tp",
				new Params.TweakNode(null, new Params.Node[]
				{
					new Params.Node("global.enable_marker_teleport", "\"True\"")
				})
			},
			{
				"small_item_in_hand",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("graphics.vm_fov_scale", "\"False\"")
				})
			},
			{
				"convenient_skin_sorting",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("client.sortskinsrecentlyused", "\"True\"")
				})
			},
			{
				"enlarged_console",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("global.consolescale", "\"16\"")
				})
			},
			{
				"left_handed",
				new Params.TweakNode(new Params.Node[]
				{
					new Params.Node("graphics.vm_horizontal_flip", "\"True\"")
				})
			}
		};

		// Token: 0x04000104 RID: 260
		public static Dictionary<string, Dictionary<int, Params.TweakNode>> Graphics = new Dictionary<string, Dictionary<int, Params.TweakNode>>
		{
			{
				"shadows",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.shadowlights", "\"1\""),
							new Params.Node("graphicssettings.shadowqualitypreset", "\"0\"")
						}, new Params.Node[]
						{
							new Params.Node("graphics.shadowmode", "\"1\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.shadowlights", "\"1\""),
							new Params.Node("graphicssettings.shadowqualitypreset", "\"0\"")
						}, new Params.Node[]
						{
							new Params.Node("graphics.shadowmode", "\"2\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.shadowlights", "\"1\""),
							new Params.Node("graphicssettings.shadowqualitypreset", "\"2\"")
						}, new Params.Node[]
						{
							new Params.Node("graphics.shadowmode", "\"2\"")
						})
					},
					{
						3,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.shadowlights", "\"1\""),
							new Params.Node("graphicssettings.shadowqualitypreset", "\"2\"")
						}, new Params.Node[]
						{
							new Params.Node("graphics.shadowmode", "\"1\"")
						})
					}
				}
			},
			{
				"textures",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphicssettings.globaltexturemipmaplimit", "\"3\""),
							new Params.Node("graphics.af", "\"1\""),
							new Params.Node("graphics.lodbias", "\"0.5\""),
							new Params.Node("graphics.shaderlod", "\"1\""),
							new Params.Node("graphicssettings.anisotropicfiltering", "\"0\""),
							new Params.Node("mesh.quality", "\"0\""),
							new Params.Node("terrain.quality", "\"100\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphicssettings.globaltexturemipmaplimit", "\"2\""),
							new Params.Node("graphics.af", "\"2\""),
							new Params.Node("graphics.lodbias", "\"0.6\""),
							new Params.Node("graphics.shaderlod", "\"2\""),
							new Params.Node("graphicssettings.anisotropicfiltering", "\"1\""),
							new Params.Node("mesh.quality", "\"30\""),
							new Params.Node("terrain.quality", "\"100\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphicssettings.globaltexturemipmaplimit", "\"1\""),
							new Params.Node("graphics.af", "\"8\""),
							new Params.Node("graphics.lodbias", "\"1\""),
							new Params.Node("graphics.shaderlod", "\"3\""),
							new Params.Node("graphicssettings.anisotropicfiltering", "\"1\""),
							new Params.Node("mesh.quality", "\"150\""),
							new Params.Node("terrain.quality", "\"100\"")
						})
					},
					{
						3,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphicssettings.globaltexturemipmaplimit", "\"0\""),
							new Params.Node("graphics.af", "\"8\""),
							new Params.Node("graphics.lodbias", "\"1\""),
							new Params.Node("graphics.shaderlod", "\"5\""),
							new Params.Node("graphicssettings.anisotropicfiltering", "\"0\""),
							new Params.Node("mesh.quality", "\"150\""),
							new Params.Node("terrain.quality", "\"100\"")
						})
					}
				}
			},
			{
				"lighting",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.contactshadows", "\"False\""),
							new Params.Node("effects.ao", "\"False\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.contactshadows", "\"False\""),
							new Params.Node("effects.ao", "\"True\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.contactshadows", "\"True\""),
							new Params.Node("effects.ao", "\"True\"")
						})
					}
				}
			},
			{
				"trees",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("tree.meshes", "\"10\""),
							new Params.Node("tree.quality", "\"30\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("tree.meshes", "\"50\""),
							new Params.Node("tree.quality", "\"100\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("tree.meshes", "\"100\""),
							new Params.Node("tree.quality", "\"150\"")
						})
					},
					{
						3,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("tree.meshes", "\"100\""),
							new Params.Node("tree.quality", "\"500\"")
						}, new Params.Node[]
						{
							new Params.Node("tree.quality", "\"500\"")
						})
					}
				}
			},
			{
				"reflections_on_the_water",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("water.quality", "\"0\""),
							new Params.Node("water.reflections", "\"0\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("water.quality", "\"0\""),
							new Params.Node("water.reflections", "\"1\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("water.quality", "\"0\""),
							new Params.Node("water.reflections", "\"2\"")
						})
					}
				}
			},
			{
				"grass",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("grass.displacement", "\"True\""),
							new Params.Node("grass.quality", "\"0\""),
							new Params.Node("graphics.grassshadows", "\"False\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("grass.displacement", "\"True\""),
							new Params.Node("grass.quality", "\"50\""),
							new Params.Node("graphics.grassshadows", "\"False\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("grass.displacement", "\"True\""),
							new Params.Node("grass.quality", "\"100\""),
							new Params.Node("graphics.grassshadows", "\"True\"")
						})
					}
				}
			},
			{
				"clouds",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.volumetric_clouds", "\"0\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.volumetric_clouds", "\"1\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("graphics.volumetric_clouds", "\"4\"")
						})
					}
				}
			},
			{
				"smoothing",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("effects.sharpen", "\"True\""),
							new Params.Node("effects.antialiasing", "\"0\"")
						})
					},
					{
						1,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("effects.sharpen", "\"True\""),
							new Params.Node("effects.antialiasing", "\"1\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("effects.sharpen", "\"True\""),
							new Params.Node("effects.antialiasing", "\"2\"")
						})
					},
					{
						3,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("effects.sharpen", "\"True\""),
							new Params.Node("effects.antialiasing", "\"3\"")
						})
					}
				}
			},
			{
				"glass_reflection",
				new Dictionary<int, Params.TweakNode>
				{
					{
						0,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("reflection.planarreflections", "\"True\"")
						})
					},
					{
						2,
						new Params.TweakNode(new Params.Node[]
						{
							new Params.Node("reflection.planarreflections", "\"False\"")
						})
					}
				}
			}
		};

		// Token: 0x0200013A RID: 314
		public class Node
		{
			// Token: 0x0600068F RID: 1679 RVA: 0x0015BED4 File Offset: 0x001596D4
			public Node(string key, string value)
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
				this.key = key;
				this.value = value;
			}

			// Token: 0x06000690 RID: 1680 RVA: 0x001516B4 File Offset: 0x0014EEB4
			public override bool Equals(object obj)
			{
				Params.Node node = obj as Params.Node;
				return node != null && P4258EBF.AFA7138A.M6233B19[250](this.key, node.key) && P4258EBF.AFA7138A.M6233B19[250](this.value, node.value);
			}

			// Token: 0x06000691 RID: 1681 RVA: 0x0015DF24 File Offset: 0x0015B724
			public override int GetHashCode()
			{
				int num = 17;
				num = num * 23 + ((this.key != null) ? P4258EBF.AFA7138A.M6233B19[87](this.key) : 0);
				return num * 23 + ((this.value != null) ? P4258EBF.AFA7138A.M6233B19[87](this.value) : 0);
			}

			// Token: 0x04000487 RID: 1159
			public string key;

			// Token: 0x04000488 RID: 1160
			public string value;
		}

		// Token: 0x0200013B RID: 315
		public class TweakNode
		{
			// Token: 0x06000692 RID: 1682 RVA: 0x0015CD68 File Offset: 0x0015A568
			public TweakNode(Params.Node[] config_params)
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
				this.config_params = config_params;
				this.launch_params = null;
			}

			// Token: 0x06000693 RID: 1683 RVA: 0x0016149C File Offset: 0x0015EC9C
			public TweakNode(Params.Node[] config_params, Params.Node[] launch_params)
			{
				P4258EBF.AFA7138A.M6233B19[130](this);
				this.config_params = config_params;
				this.launch_params = launch_params;
			}

			// Token: 0x04000489 RID: 1161
			public Params.Node[] config_params;

			// Token: 0x0400048A RID: 1162
			public Params.Node[] launch_params;
		}
	}
}

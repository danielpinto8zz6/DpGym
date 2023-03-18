<script lang="ts">
	import { Badge, Menu, Divider } from 'stwui';
	import { page } from '$app/stores';
	import { onMount } from 'svelte/internal';

	interface Item {
		title: string;
		href: string;
		beta?: boolean;
		updated?: boolean;
	}

	const sidebarItems: Item[] = [
		{
			title: 'Members',
			href: '/members'
		}
	];

	export let handleClose: (() => void) | undefined = undefined;

	let active = '';

	function handleClick(item: string) {
		active = item;
		if (handleClose) {
			handleClose();
		}
	}

	const menuTitleClass = 'text-xs font-bold text-primary py-2 px-3 h-10';

	onMount(() => {
		active = $page.url.pathname;
	});
</script>

<h3 class={menuTitleClass}>COMPONENTS</h3>
<Menu {active}>
	{#each sidebarItems as item}
		{#if item.beta}
			<Menu.Item
				key={item.href}
				label={item.title}
				href={item.href}
				on:click={() => handleClick(item.href)}
			>
				<Badge slot="extra" type="error">BETA</Badge>
			</Menu.Item>
		{:else if item.updated}
			<Menu.Item
				key={item.href}
				label={item.title}
				href={item.href}
				on:click={() => handleClick(item.href)}
			>
				<Badge slot="extra" type="success">UPDATED</Badge>
			</Menu.Item>
		{:else}
			<Menu.Item
				key={item.href}
				label={item.title}
				href={item.href}
				on:click={() => handleClick(item.href)}
			/>
		{/if}
	{/each}
</Menu>

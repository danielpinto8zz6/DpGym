<script lang="ts">
	import '../app.css';
	import { theme } from '../lib/stores';
	import { page } from '$app/stores';
	import {
		Button,
		Drawer,
		Swap,
		Icon,
		Layout,
		Portal,
		Row,
		Toggle,
		Col,
		Divider
	} from 'stwui';
	import { browser } from '$app/environment';
	import { Navigation } from '../lib/';
	import { menu, close, brightness_4, brightness_5 } from '../lib/icons';

	let openMenu = false;

	function handleOpenMenu() {
		openMenu = !openMenu;
	}

	function closeOpenMenu() {
		openMenu = false;
	}

	$: darkTheme = $theme === 'dark';
	$: if (browser && darkTheme) {
		const htmlElement = document.documentElement;
		htmlElement.classList.add('dark');
		theme.set('dark');
	} else if (browser) {
		const htmlElement = document.documentElement;
		htmlElement.classList.remove('dark');
		theme.set('light');
	}

	let path = $page.url.pathname;

	async function scrollToTop(pathname: string) {
		const newPath = pathname;
		if (path !== newPath) {
			if (browser) {
				const scrollEl = document.getElementById('content-body') as HTMLDivElement;
				if (scrollEl) {
					scrollEl.scrollTop = 0;
				}
			}
		}
		path = newPath;
	}

	$: scrollToTop($page.url.pathname);
</script>

<svelte:head>
	{#if darkTheme}
		<meta name="theme-color" content="#242526" />
	{:else}
		<meta name="theme-color" content="#ffffff" />
	{/if}
	<title>{$page.data.title}</title>
	<meta name="description" content={$page.data.description} />
	<!-- TODO:  figure out canonical rel -->
	<!-- <link rel="canonical" href="https://madewithsvelte.com/ui-library"> -->
	<meta name="og:title" content={$page.data.title} />
	<meta property="og:description" content={$page.data.description} />
	<!-- TODO: add url to all page load functions -->
	<!-- <meta property="og:url" content={$page.data.url}> -->
	<meta name="twitter:title" content={$page.data.title} />
	<meta name="twitter:description" content={$page.data.description} />
</svelte:head>

<div class="h-full w-full print:hidden">
	<Layout>
		<div
			class="fixed top-0 left-0 right-0 h-[var(--sat)] z-10 bg-light-surface dark:bg-dark-surface shadow-md dark:shadow-black"
		/>
		<Layout.Header>
			<Button
				ariaLabel="open menu"
				shape="circle"
				class="inline-block lg:hidden mr-4 bg-light-icon-background text-light-icon dark:bg-dark-icon-background dark:text-dark-icon border-none outline-none"
				on:click={handleOpenMenu}
			>
				<Swap slot="icon">
					<Icon slot="on" data={menu} stroke="currentColor" />
					<Icon slot="off" data={close} stroke="currentColor" />
				</Swap>
			</Button>

			<a href="/">
				<img
					src="/120x120-transparent.png"
					alt="logo-icon"
					class="h-10 mr-1"
					width="40px"
					height="40px"
				/>
			</a>

			<div class="font-bold text-xl opacity-80 dark:opacity-100">STWUI</div>

			<Layout.Header.Extra slot="extra">
				<Toggle name="toggle" bind:on={darkTheme}>
					<Toggle.LeftIcon slot="left-icon" data={brightness_5} />
					<Toggle.RightIcon slot="right-icon" data={brightness_4} />
				</Toggle>

				<Button
					ariaLabel="open github"
					href="https://github.com/N00nDay/stwui"
					shape="circle"
					class="ml-2 bg-light-icon-background dark:bg-dark-icon-background text-light-icon dark:text-dark-icon"
				>
					<svg
						xmlns="http://www.w3.org/2000/svg"
						x="0px"
						y="0px"
						viewBox="0 0 512 512"
						class="svg-icon inline-block outline-none h-5 w-5 fill-current"
						focusable="false"
						data-testid="svg-icon"
						><path
							d="M165.9 397.4c0 2-2.3 3.6-5.2 3.6-3.3.3-5.6-1.3-5.6-3.6 0-2 2.3-3.6 5.2-3.6 3-.3 5.6 1.3 5.6 3.6zm-31.1-4.5c-.7 2 1.3 4.3 4.3 4.9 2.6 1 5.6 0 6.2-2s-1.3-4.3-4.3-5.2c-2.6-.7-5.5.3-6.2 2.3zm44.2-1.7c-2.9.7-4.9 2.6-4.6 4.9.3 2 2.9 3.3 5.9 2.6 2.9-.7 4.9-2.6 4.6-4.6-.3-1.9-3-3.2-5.9-2.9zM244.8 8C106.1 8 0 113.3 0 252c0 110.9 69.8 205.8 169.5 239.2 12.8 2.3 17.3-5.6 17.3-12.1 0-6.2-.3-40.4-.3-61.4 0 0-70 15-84.7-29.8 0 0-11.4-29.1-27.8-36.6 0 0-22.9-15.7 1.6-15.4 0 0 24.9 2 38.6 25.8 21.9 38.6 58.6 27.5 72.9 20.9 2.3-16 8.8-27.1 16-33.7-55.9-6.2-112.3-14.3-112.3-110.5 0-27.5 7.6-41.3 23.6-58.9-2.6-6.5-11.1-33.3 2.6-67.9 20.9-6.5 69 27 69 27 20-5.6 41.5-8.5 62.8-8.5s42.8 2.9 62.8 8.5c0 0 48.1-33.6 69-27 13.7 34.7 5.2 61.4 2.6 67.9 16 17.7 25.8 31.5 25.8 58.9 0 96.5-58.9 104.2-114.8 110.5 9.2 7.9 17 22.9 17 46.4 0 33.7-.3 75.4-.3 83.6 0 6.5 4.6 14.4 17.3 12.1C428.2 457.8 496 362.9 496 252 496 113.3 383.5 8 244.8 8zM97.2 352.9c-1.3 1-1 3.3.7 5.2 1.6 1.6 3.9 2.3 5.2 1 1.3-1 1-3.3-.7-5.2-1.6-1.6-3.9-2.3-5.2-1zm-10.8-8.1c-.7 1.3.3 2.9 2.3 3.9 1.6 1 3.6.7 4.3-.7.7-1.3-.3-2.9-2.3-3.9-2-.6-3.6-.3-4.3.7zm32.4 35.6c-1.6 1.3-1 4.3 1.3 6.2 2.3 2.3 5.2 2.6 6.5 1 1.3-1.3.7-4.3-1.3-6.2-2.2-2.3-5.2-2.6-6.5-1zm-11.4-14.7c-1.6 1-1.6 3.6 0 5.9 1.6 2.3 4.3 3.3 5.6 2.3 1.6-1.3 1.6-3.9 0-6.2-1.4-2.3-4-3.3-5.6-2z"
						/></svg
					>
				</Button>
			</Layout.Header.Extra>
		</Layout.Header>

		<Layout.Content
			class="h-[calc(100%-64px)] bg-light-background dark:bg-dark-background"
			expandedWidth="17rem"
		>
			<Layout.Content.Sidebar class="max-w-full">
				<Navigation />
			</Layout.Content.Sidebar>
			<Layout.Content.Body
				id="content-body"
				class="relative h-full w-full overflow-x-hidden overflow-y-auto pt-[calc(1rem+var(--sat))] pb-[calc(1rem+var(--sab))] pr-[calc(1rem+var(--sar))] pl-[calc(1rem+var(--sal))] md:pt-[calc(2rem+var(--sat))] md:pb-[calc(2rem+var(--sab))] md:pr-[calc(2rem+var(--sar))] md:pl-[calc(2rem+var(--sal))]"
			>
				<div>
					<Row gutter="3" class="h-full w-full">
						{#if $page.data.header}
							<Col class="col-24 mb-4">
								<h1 class="text-light-content dark:text-dark-content mb-0">{$page.data.header}</h1>
								{#if $page.data.subHeader}
									<p class="text-light-secondary-content dark:text-dark-secondary-content">
										{$page.data.subHeader}
									</p>
								{/if}
								<Divider />
							</Col>
						{/if}
						<slot />
					</Row>
				</div>
			</Layout.Content.Body>
		</Layout.Content>
	</Layout>
</div>

<Portal>
	{#if openMenu}
		<Drawer handleClose={closeOpenMenu} placement="left">
			<Drawer.Content class="overflow-y-auto p-4">
				<Navigation handleClose={closeOpenMenu} />
			</Drawer.Content>
		</Drawer>
	{/if}
</Portal>
